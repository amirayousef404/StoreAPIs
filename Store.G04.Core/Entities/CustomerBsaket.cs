using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities
{
    public class CustomerBsaket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
