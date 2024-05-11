using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business.CategoryModule.DTO
{
    public class ItemResponseDto:BaseObjectDto
    {
        public string ItemId { get; set; }  
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal? Price {  get; set; }
        public string UploadedBy { get; set; }

    }
}
