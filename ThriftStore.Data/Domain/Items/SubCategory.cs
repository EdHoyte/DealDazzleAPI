using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Data.Domain.Items
{
    public class SubCategory : BaseObject
    {
        public string SubCategoryName { get; set; }
        [ForeignKey("CategoryId")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Item> Items { get; set; }= new HashSet<Item>();

    }
}
