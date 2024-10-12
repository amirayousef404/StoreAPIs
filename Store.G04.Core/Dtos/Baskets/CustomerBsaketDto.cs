using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Dtos.Baskets
{
    public class CustomerBsaketDto
    {
        public int Id { get; set; }

        public List<BasketItem> Items { get; set; }
    }
}
