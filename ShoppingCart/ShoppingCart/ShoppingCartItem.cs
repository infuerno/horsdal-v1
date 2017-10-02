using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Money Price { get; set; }
    }

    public class Money
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
    }

}
