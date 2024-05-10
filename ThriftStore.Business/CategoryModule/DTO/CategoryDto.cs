using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business.CategoryModule.DTO
{
    public class CategoryDto:BaseObjectDto
    {
        public string CategoryName { get; set; }
        public Object T {  get; set; }

    }
}
