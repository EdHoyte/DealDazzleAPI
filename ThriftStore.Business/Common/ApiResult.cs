using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business.Common
{
    public class ApiResult<T>
    {
        public T Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }

    public class ApiResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }

}
