
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class SaleDetails
    {
        public Guid Id { get; set; }
        public PropertyDetails Property { get; set; }
        public String Saler { get; set; }
        public String Buyer { get; set; }

    }
}
