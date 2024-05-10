using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business.UserModule.Interface
{
    public interface IAuthUser
    {
        string UserId { get; }
    }
}
