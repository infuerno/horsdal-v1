using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace ShoppingCart
{
    public interface IShoppingCartStore
    {
        ShoppingCart Get(int userId);
        void Save(ShoppingCart shoppingCart);
    }

    // handles reading and updating shopping carts in the data store
    public class ShoppingCartStore : IShoppingCartStore
    {
        public ShoppingCart Get(int userId)
        {
            return new ShoppingCart();
        }

        public void Save(ShoppingCart shoppingCart)
        {
        }
    }
}
