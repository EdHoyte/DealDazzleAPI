using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Data.Domain;
using ThriftStore.Data.Domain.Items;

namespace ThriftStore.Business.CategoryModule.DTO
{
    public class SubCategoryDto:BaseObjectDto
    {
        public string SubCategoryName { get; set; }
        //[ForeignKey("Category")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public Collection<Item> Items { get; set; }
    }
}
