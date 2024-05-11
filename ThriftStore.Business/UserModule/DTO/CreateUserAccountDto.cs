using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business.UserModule.DTO
{
    public class CreateUserAccountDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
    }
}
