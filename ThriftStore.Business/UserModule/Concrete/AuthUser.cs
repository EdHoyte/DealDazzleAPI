using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Business.UserModule.Interface;

namespace ThriftStore.Business.UserModule.Concrete
{
    public class AuthUser : IAuthUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId
        {
            get
            {
                return _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
