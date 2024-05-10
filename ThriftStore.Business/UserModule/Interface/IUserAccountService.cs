using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Business.Common;
using ThriftStore.Business.UserModule.DTO;

namespace ThriftStore.Business.UserModule.Interface
{
    public interface IUserAccountService
    {
        Task<ApiResult<CreateUserAccountResponseDto>> CreateUserAccount(CreateUserAccountDto model); 
        Task<ApiResult<MessageResponse>> LoginUserAccount (LoginUserAccountDto model);
        Task<ApiResult<MessageResponse>> LogoutUserAccount();
        string UserId { get; }
    }
}
