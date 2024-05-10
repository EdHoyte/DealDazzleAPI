using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business.CategoryModule.DTO
{
	public class ItemDto:BaseObjectDto
	{
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public long SubCategory {  get; set; }
		public string ItemName { get; set; }
		public decimal Price { get; set; }
		public List<string> ImageUrls { get; set; }= new List<string>();
	}
}
