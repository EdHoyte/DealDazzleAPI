using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Data.Domain.Items;

namespace ThriftStore.Business.CategoryModule.DTO
{
    public class CreateItemDto:BaseObjectDto
    {
        public string ItemName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public long SubCategoryId { get; set; }
        public long CategoryId { get; set; }
        public List<CreateItemImageDto> Images { get; set; }=new List<CreateItemImageDto>();

    }
}
