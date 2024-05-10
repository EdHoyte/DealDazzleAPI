using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Data.Domain.Items
{
    public class ItemImage : BaseObject
    {
        [MaxLength(255)]
        public string FileName { get; set; }
        [ForeignKey("ItemId")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }
    }

}
