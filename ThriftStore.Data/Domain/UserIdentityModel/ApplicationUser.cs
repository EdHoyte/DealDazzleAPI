using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Data.Domain.Items;

namespace ThriftStore.Data.Domain.Users
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        //public string PhoneNumber {  get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<Item> Items { get; set; }=new HashSet<Item>();
    }
}
