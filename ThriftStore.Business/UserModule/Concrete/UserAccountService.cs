using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Business.Common;
using ThriftStore.Business.UserModule.DTO;
using ThriftStore.Business.UserModule.Interface;
using ThriftStore.Data;
using ThriftStore.Data.Domain.Users;

namespace ThriftStore.Business.UserModule.Concrete
{
    public class ApplicationUserEventArgs : EventArgs
    {
        public ApplicationUser User { get; set; }
    }
    public class UserAccountService : IUserAccountService
    {
        private readonly StoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UserAccountService> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;



        public UserAccountService(StoreDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<UserAccountService> logger, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }

        public event EventHandler<ApplicationUserEventArgs> OnUserCreate;

        private void OnCreate(ApplicationUser user)
        {
            if (OnUserCreate != null)
            {
                OnUserCreate(this, new() { User = user });
            }
        }
        public string UserId
        {
            get
            {
                return _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        public async Task<ApiResult<CreateUserAccountResponseDto>> CreateUserAccount(CreateUserAccountDto model)
        {
            ApiResult<CreateUserAccountResponseDto> result = new() { Result = new(), StatusCode = System.Net.HttpStatusCode.BadRequest };
            try
            {
                //Create User
                ApplicationUser user = new()
                {
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    Email = model.Email,
                    EmailConfirmed = false,
                    FullName = model.FullName,
                    LockoutEnabled = true,
                    PhoneNumberConfirmed = false,
                    UserName = model.Email,
                    PhoneNumber = model.Phone
                };


                IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password);
                if (!identityResult.Succeeded)
                {
                    result.Message = identityResult.Errors.Select(x=>x.Description).FirstOrDefault();
                    return result;
                }
                //Check if a Role called User is in existence. If not, create it
                ApplicationRole applicationRole = await _roleManager.FindByNameAsync(model.Role);
                if (applicationRole == null)
                {
                    applicationRole = new() { Name = model.Role };
                    identityResult = await _roleManager.CreateAsync(applicationRole);
                    if (!identityResult.Succeeded)
                    {
                        result.Message = identityResult.Errors.Select(x => x.Description).FirstOrDefault();
                        return result;
                    }
                }

                //Add the newly cfreated User to the role
                identityResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!identityResult.Succeeded)
                {
                    result.Message = identityResult.Errors.Select(x => x.Description).FirstOrDefault();
                    return result;
                }

                result.Message = "User was added successfully";
                result.IsSuccessful = true;
                result.StatusCode = System.Net.HttpStatusCode.OK;
                result.Result.UserId = user.Id;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                result.Message = "An error occurred.";
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }
        }

        public async Task<ApiResult<MessageResponse>> LoginUserAccount(LoginUserAccountDto model)
        {
            ApiResult<MessageResponse> response = new();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                response.Message = "Email does not exist";
                return response;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);
                response.IsSuccessful = true;
                response.Message = "Login successful";
                return response;
            }
            if (signInResult.IsNotAllowed)
            {
                response.Message = "Sign in not allowed";
                return response;
            }
            if (signInResult.IsLockedOut)
            {
                response.Message = "You have been locked out, please try again later";
                return response;
            }
            response.Message = "Unkown error";
            return response;
        }

        public async Task<ApiResult<MessageResponse>> LogoutUserAccount()
        {
            await _signInManager.SignOutAsync();
            return new ApiResult<MessageResponse> { Message = "Logout successful", IsSuccessful = true };
        }

    }
}
