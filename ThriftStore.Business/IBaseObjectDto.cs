﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Business
{
    public interface IBaseObjectDto
    {
        bool IsActive { get; }
        DateTime CreatedDate { get; }
        bool IsDeleted { get; }
    }
}
