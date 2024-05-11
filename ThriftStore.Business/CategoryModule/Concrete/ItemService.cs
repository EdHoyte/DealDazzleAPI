using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Business.CategoryModule.DTO;
using ThriftStore.Business.CategoryModule.Interface;
using ThriftStore.Business.Common;
using ThriftStore.Business.UserModule.DTO;
using ThriftStore.Business.UserModule.Interface;
using ThriftStore.Data;
using ThriftStore.Data.Domain;
using ThriftStore.Data.Domain.Items;
using ThriftStore.Data.Domain.Users;

namespace ThriftStore.Business.CategoryModule.Concrete
{
	public class InvestmentEventArgs : EventArgs
	{
		public Data.Domain.Item Items { get; set; }
	}
	public class ItemService : IItemService
	{
		private readonly StoreDbContext _context;
		private readonly IUserAccountService _userAccountService;
		private readonly ILogger<ItemService> _logger;
		private UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAuthUser _user;
		private readonly IHostingEnvironment _webHost;

		public ItemService(StoreDbContext dbContext, IUserAccountService userAccountService, ILogger<ItemService> logger, UserManager<ApplicationUser> userManager, IAuthUser user, IHttpContextAccessor httpContextAccessor, IHostingEnvironment webHost)
		{
			_context = dbContext;
			_userAccountService = userAccountService;
			_logger = logger;
			_userManager = userManager;
			_user = user;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
			
		}

		#region Item
		public async Task<ApiResult<ItemResponseDto>> AddOrUpdateItem(CreateItemDto model)
		{
			ApiResult<ItemResponseDto> result = new() { Result = new(), StatusCode = System.Net.HttpStatusCode.BadRequest };

			try
			{

				ApplicationUser appUser = await _userManager.FindByIdAsync(_userAccountService.UserId);
				if (appUser == null)
				{
					result.Message = "Please Register or Login to upload an item.";
					return result;
				}
				Item item;
				if (model.Id == 0)
				{
					 var extensionList = model.Images.Select(z => z.ImageExtension).ToList();
					if (extensionList.Count > 0)
					{
						foreach (var ext in extensionList)
						{
							if (!ext.StartsWith("."))
							{
								result.Message = extensionList.Count == 1 ? "Invalid extension in your image upload" : "Invalid extension in one of your uploaded images";
							}
						}
					}
					item = new Item
					{
						ItemName = model.ItemName,
						Description = model.Description,
						Price = model.Price,
						SubCategoryId = model.SubCategoryId,
						CreatedBy = appUser.Id
					};
					_context.Items.Add(item);
					await _context.SaveChangesAsync();
					if (model.Images.Count > 0)
					{
						string directory = Path.Combine(_webHost.ContentRootPath, "ProductImages");
						if(!Directory.Exists(directory))
							Directory.CreateDirectory(directory);

						string fileName = string.Empty;
						string filePath = string.Empty;
						foreach (var image in model.Images)
						{
							fileName = Guid.NewGuid().ToString() + image.ImageExtension;
							filePath = Path.Combine(directory, fileName);
							byte[] bytes = System.Convert.FromBase64String(image.ImageData);
							File.WriteAllBytes(filePath, bytes);

							ItemImage itemImage = new ItemImage
							{
								CreatedBy = appUser.Id,
								FileName = fileName,
								ItemId = item.Id
							};
							_context.ItemImages.Add(itemImage);
						}
						_context.SaveChanges();
					}
				}
				else
				{
					item = await _context.Items.FindAsync(model.Id);
					if (item == null)
						result.Message = "Item  Not Found";
					result.IsSuccessful = false;
					result.StatusCode = System.Net.HttpStatusCode.NotFound;
					return result;
				}
				item.UserId = _userAccountService.UserId;
				item.ItemName = model.ItemName;
				item.Description = model.Description;
				item.Price = model.Price;
				item.SubCategoryId = model.SubCategoryId;
				item.CreatedBy = item.UserId;

				await _context.SaveChangesAsync();

				return new ApiResult<ItemResponseDto>
				{
					Result = new ItemResponseDto
					{
						ItemId = item.Id.ToString(),
						ItemName = item.ItemName,
						Description = item.Description,
						Price = item.Price,
						UploadedBy = appUser.FullName,
						CreatedDate = DateTime.Now
					},
					IsSuccessful = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};



			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred");
				return new ApiResult<ItemResponseDto>
				{
					Message = "Failed to add or update item.",
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.InternalServerError
				};
			}
		}

		public async Task<ApiResult<MessageResponse>> DeleteItem(long id)
		{
			ApiResult<MessageResponse> result = new() { Result = new(), StatusCode = System.Net.HttpStatusCode.BadRequest };

			try
			{
				var item = await _context.Items.FindAsync(id);
				if (item == null)
				{
					result.Message = "Item  Not Found";
					result.IsSuccessful = false;
					result.StatusCode = System.Net.HttpStatusCode.NotFound;
					return result;
				}
				else
				{
					_context.Items.Remove(item);
					await _context.SaveChangesAsync();
				}
				return new ApiResult<MessageResponse>
				{
					Message = "Item deleted successfully",
					IsSuccessful = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An Error occurred");
				return new ApiResult<MessageResponse>
				{
					Message = "Failed to delete item.",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.NotFound
				};

				//_logger.LogError(ex, );
				//return ApiResult.Failed("Failed to delete item.");
			}
		}

		public async Task<ApiResult<CreateItemDto>> GetSingleItem(long id)
		{
			ApiResult<CreateItemDto> result = new() { Result = new(), StatusCode = System.Net.HttpStatusCode.BadRequest };

			try
			{
				var item = await _context.Items.FindAsync(id);
				if (item == null)
				{

					result.Message = "Item  Not Found";
					result.IsSuccessful = false;
					result.StatusCode = System.Net.HttpStatusCode.NotFound;
					return result;
				}
				else
				{

				}

				return new ApiResult<CreateItemDto>
				{
					Result = new CreateItemDto
					{
						Id = item.Id,
						SubCategoryId = item.SubCategoryId,
						ItemName = item.ItemName,
						Description = item.Description,
						Price = item.Price,
						CreatedDate = DateTime.Now
					},
					IsSuccessful = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred");
				return new ApiResult<CreateItemDto>
				{
					Message = "Item not found",
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.InternalServerError
				};
			}
		}

		public async Task<ApiResult<IEnumerable<CreateItemDto>>> GetAllItems()
		{
			ApiResult<IEnumerable<CreateItemDto>> result = new() { StatusCode = System.Net.HttpStatusCode.BadRequest };
			try
			{
				var items = await _context.Items.AsNoTracking().ToListAsync();

				var itemDtos = items.Select(item => new CreateItemDto
				{
					Id = item.Id,
					SubCategoryId = item.SubCategoryId,
					CreatedDate = item.CreatedDate,
					ItemName = item.ItemName,
					Description = item.Description,
					Price = item.Price
				}).AsEnumerable();

				return new ApiResult<IEnumerable<CreateItemDto>>
				{
					Result = itemDtos,
					IsSuccessful = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to retrieve items.");
				return new ApiResult<IEnumerable<CreateItemDto>>
				{
					Message = "Failed to retrieve items.",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.InternalServerError
				};
			}
		}
		#endregion

		#region SubCategory
		public async Task<ApiResult<IEnumerable<SubCategoryDto>>> GetAllSubCategories()
		{
			ApiResult<IEnumerable<SubCategoryDto>> result = new() { StatusCode = System.Net.HttpStatusCode.BadRequest };
			try
			{
				var subCategory = await _context.SubCategories.AsNoTracking().ToListAsync();

				var subCategories = subCategory.Select(subCategory => new SubCategoryDto
				{
					SubCategoryName = subCategory.SubCategoryName,
					CategoryId = subCategory.CategoryId,
					Id = subCategory.Id
				}).AsEnumerable();

				return new ApiResult<IEnumerable<SubCategoryDto>>
				{
					Result = subCategories,
					IsSuccessful = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred.");
				return new ApiResult<IEnumerable<SubCategoryDto>>
				{
					Message = "An error occurred, please try again",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.InternalServerError
				};
			}
		}
		public async Task<ApiResult<IEnumerable<CreateItemDto>>> GetItemBySubCategory(long subCategoryid)
		{
			ApiResult<IEnumerable<CreateItemDto>> result = new() { StatusCode = System.Net.HttpStatusCode.BadRequest };

			try
			{

				var subCategory = await _context.SubCategories.FindAsync(subCategoryid);
				if (subCategory == null)
				{
					return new ApiResult<IEnumerable<CreateItemDto>>
					{
						Message = "SubCategory not found",
						IsSuccessful = false,
						Result = null,
						StatusCode = System.Net.HttpStatusCode.NotFound
					};
				}
				else
				{
					var items = await _context.Items
					.Where(item => item.SubCategoryId == subCategoryid)
					.AsNoTracking()
					.ToListAsync();

					var itemDtos = items.Select(item => new CreateItemDto
					{
						ItemName = item.ItemName,
						Description = item.Description,
						Price = item.Price,
						Id = item.Id,
						SubCategoryId = subCategoryid

					}).AsEnumerable();

					return new ApiResult<IEnumerable<CreateItemDto>>
					{
						Result = itemDtos,
						IsSuccessful = true,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to retrieve items");
				return new ApiResult<IEnumerable<CreateItemDto>>
				{
					Message = "Unable to retrieve items",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.NotFound
				};
			}
		}
		#endregion

		#region Category
		public async Task<ApiResult<CategoryDto>> GetCategory(long id)
		{
			ApiResult<CategoryDto> result = new() { StatusCode = System.Net.HttpStatusCode.BadRequest };
			try
			{

				var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
				if (category == null)
				{
					return new ApiResult<CategoryDto>
					{
						Message = "Category not found",
						IsSuccessful = false,
						Result = null,
						StatusCode = System.Net.HttpStatusCode.NotFound
					};
				}
				else
				{

					var response = new CategoryDto
					{
						CategoryName = category.CategoryName,
						Id = category.Id
					};

					return new ApiResult<CategoryDto>
					{
						Result = response,
						IsSuccessful = true,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to retrieve category");
				return new ApiResult<CategoryDto>
				{
					Message = "Failed to retrieve category",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.InternalServerError
				};
			}
		}

		public async Task<ApiResult<IEnumerable<CategoryDto>>> GetAllCategory()
		{
			ApiResult<IEnumerable<CategoryDto>> result = new() { StatusCode = System.Net.HttpStatusCode.BadRequest };
			try
			{
				var categories = await _context.Categories.AsNoTracking().ToListAsync();

				var Categories = categories.Select(category => new CategoryDto
				{
					CategoryName = category.CategoryName,
					Id = category.Id

				}).AsEnumerable();

				return new ApiResult<IEnumerable<CategoryDto>>
				{
					Result = Categories,
					IsSuccessful = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred.");
				return new ApiResult<IEnumerable<CategoryDto>>
				{
					Message = "Failed to retrieve categories, please try again",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.InternalServerError
				};
			}
		}

		#endregion

		public string GetFileExtensionFromBase64(string base64String)
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResult<IEnumerable<CreateItemDto>>> SearchItems(string keyword)
		{
			ApiResult<IEnumerable<CreateItemDto>> result = new() { StatusCode = System.Net.HttpStatusCode.BadRequest };

			try
			{
				if (string.IsNullOrWhiteSpace(keyword))
				{
					result.Message = "Abeg tell me wetin you dey find, make I bring am come out for you.";
					result.IsSuccessful = false;
					result.StatusCode = System.Net.HttpStatusCode.NotFound;
					return result;
				}
				else
				{
					keyword = keyword.ToLower();
					var items = await _context.Items
						.Where(item => item.ItemName.ToLower().Contains(keyword) || item.Description.ToLower().Contains(keyword))
						.AsNoTracking()
						.ToListAsync();

					var itemDtos = items.Select(item => new CreateItemDto
					{
						Id = item.Id,
						ItemName = item.ItemName,
						Description = item.Description,
						Price = item.Price
					}).AsEnumerable();

					return new ApiResult<IEnumerable<CreateItemDto>>
					{
						Result = itemDtos,
						IsSuccessful = true,
						StatusCode = System.Net.HttpStatusCode.OK
					};
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to retrieve items");
				return new ApiResult<IEnumerable<CreateItemDto>>
				{
					Message = $"Sorry! {keyword} does not exist",
					Result = null,
					IsSuccessful = false,
					StatusCode = System.Net.HttpStatusCode.NotFound
				};
			}
		}
	}
}
