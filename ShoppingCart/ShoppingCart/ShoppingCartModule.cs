using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;

namespace ShoppingCart
{
    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule(IShoppingCartStore shoppingCartStore,
            IProductCatalogClient productCatalog,
            IEventStore eventStore)
            : base("/shoppingcart")
        {
            // get a specific user's shopping basket
            // route declaration, path followed by route handler
            Get("/{userid:int}", parameters =>
            {
                var userId = (int) parameters.userid;
                return shoppingCartStore.Get(userId);
            });

            Post("/{userid:int}/items", async (parameters, _) =>
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int) parameters.userid;

                var shoppingCart = shoppingCartStore.Get(userId);
                // get up to date information about the items chosen from the product catalog (including price)
                var shoppingCartItems = await productCatalog
                    .GetShoppingCartItems(productCatalogIds)
                    .ConfigureAwait(false);
                shoppingCart.AddItems(shoppingCartItems, eventStore);
                shoppingCartStore.Save(shoppingCart);

                return shoppingCart;
            });

            Delete("/{userid:int}/items", parameters =>
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int) parameters.userid;

                var shoppingCart = shoppingCartStore.Get(userId);
                shoppingCart.RemoveItems(productCatalogIds, eventStore);
                shoppingCartStore.Save(shoppingCart);

                return shoppingCart;
            });
        }
    }
}
    