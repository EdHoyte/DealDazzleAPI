using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Data.Domain.Items;
using ThriftStore.Data.Domain.Users;

namespace ThriftStore.Data.Domain
{
    public class Item:BaseObject
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [ForeignKey("SubCategoryId")]
        public long SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
