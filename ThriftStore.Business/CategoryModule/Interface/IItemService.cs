using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Business.CategoryModule.DTO;
using ThriftStore.Business.Common;

namespace ThriftStore.Business.CategoryModule.Interface
{
    public interface IItemService
    {
        #region Utility
        string GetFileExtensionFromBase64(string base64String);
        #endregion

        #region Category
        Task<ApiResult<IEnumerable<CategoryDto>>> GetAllCategory();
        Task<ApiResult<CategoryDto>> GetCategory(long id);

        #endregion

        #region SubCategory
        Task<ApiResult<IEnumerable<CreateItemDto>>> GetItemBySubCategory(long subCategoryid);
        Task<ApiResult<IEnumerable<SubCategoryDto>>> GetAllSubCategories();
        #endregion

        #region Items

        Task<ApiResult<ItemResponseDto>> AddOrUpdateItem(CreateItemDto model);
        Task<ApiResult<CreateItemDto>> GetSingleItem(long id);
        Task<ApiResult<MessageResponse>> DeleteItem(long id);
        Task<ApiResult<IEnumerable<CreateItemDto>>> GetAllItems();
        Task<ApiResult<IEnumerable<CreateItemDto>>> SearchItems(string keyword);
        //Task<ApiResult<IEnumerable<string>>>UploadImages(List<IFormFile> files);

        #endregion
    }
}
