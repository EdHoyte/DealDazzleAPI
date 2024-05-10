using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftStore.Data.Enums
{
    public enum BuyerRole
    {
        Regular =1,
        Premium,
        VIP
    }

    public enum SellerRole
    {
        Individual = 1,
        Store,
        Verified
    }
}
