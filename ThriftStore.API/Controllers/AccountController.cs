using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThriftStore.Business.CategoryModule.DTO;
using ThriftStore.Business.CategoryModule.Interface;
using ThriftStore.Business.UserModule.DTO;
using ThriftStore.Business.UserModule.Interface;

namespace ThriftStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IItemService _itemService;

        public AccountController(IUserAccountService userAccountService, IItemService itemService)
        {
            _userAccountService = userAccountService;
            _itemService = itemService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserAccountDto model)
        {

            var result = await _userAccountService.CreateUserAccount(model);
            return StatusCode((int)result.StatusCode, result);

        }

        [HttpPost("upload-item")]
        [Authorize]
        public async Task<IActionResult> UploadItem(CreateItemDto model)
        {
            var result = await _itemService.AddOrUpdateItem(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginUserAccountDto model)
        {
            var result = await _userAccountService.LoginUserAccount(model);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("logout")]

        public async Task<IActionResult> Logout()
        {
            var result = await _userAccountService.LogoutUserAccount();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
