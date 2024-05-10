using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Data
{
    public class BaseObject
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get;} = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; } = true;
        public bool IsDeleted { get; } = false;
    }
}
