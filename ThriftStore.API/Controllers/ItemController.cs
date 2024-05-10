using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftStore.Business.CategoryModule.Interface;
using ThriftStore.Data.Domain.Items;

namespace ThriftStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        #region Category

        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _itemService.GetAllCategory();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("get-category-by-id")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _itemService.GetCategory(id);
            return StatusCode((int)result.StatusCode, result);
        }
        #endregion

        #region SubCategory

        [HttpGet("get-subcategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            var result = await _itemService.GetAllSubCategories();
            return StatusCode((int)result.StatusCode, result);
        }

        //[HttpGet("get-subcategory-by-id")]
        //public async Task<IActionResult> GetSubCategoryById(int id)
        //{
        //    var result = await _itemService.GetBySubCategory(id);
        //    return StatusCode((int)result.StatusCode, result);
        //}
        #endregion

        #region Items

        [HttpGet("get-items")]
        public async Task<IActionResult> GetAllItems()
        {
            var result = await _itemService.GetAllItems();
            return StatusCode((int)result.StatusCode, result);
        }


        [HttpGet("get-item-by-id")]
        public async Task<IActionResult> GetItem(int id)
        {
            var result = await _itemService.GetSingleItem(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItem(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("get-items-by-subcategory")]
        public async Task<IActionResult> GetItemBySubCategory(int subCategoryId)
        {
            var result = await _itemService.GetItemBySubCategory(subCategoryId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchWord)
        {
           
            try
            {
                var result = await _itemService.SearchItems(searchWord);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Abeg one error don occur as I dey search for wetin you want.");
            }
            
        }
        #endregion


    }
}
