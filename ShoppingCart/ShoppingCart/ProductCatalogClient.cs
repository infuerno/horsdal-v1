using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoppingCart
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productIds);
    }

    public class ProductCatalogClient : IProductCatalogClient
    {
        private static string productCatalogueBaseUrl = "http://private-aa1b31-infuerno.apiary-mock.com";
        private static string getProductPathTemplate = "/products"; // "/products?productIds=[{0}]"

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds)
        {
            return await GetItemsFromCatalogueService(productCatalogueIds);
        }

        private async Task<IEnumerable<ShoppingCartItem>> GetItemsFromCatalogueService(int[] productCatalogueIds)
        {
            var response = await RequestProductFromProductCatalogue(productCatalogueIds)
                .ConfigureAwait(false);
            return await ConvertToShoppingCartItems(response)
                .ConfigureAwait(false);
        }

        private static async Task<HttpResponseMessage> RequestProductFromProductCatalogue(int[] productCatalogueIds)
        {
            var productsResource = string.Format(getProductPathTemplate, string.Join(",", productCatalogueIds));

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productCatalogueBaseUrl);
                return await httpClient.GetAsync(productsResource).ConfigureAwait(false);
            }
        }

        private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var products = JsonConvert.DeserializeObject<List<ProductCatalogueProduct>>(
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false));

            return products
                    .Select(p => new ShoppingCartItem() {                        
                        ProductId = int.Parse(p.ProductId),
                        ProductName = p.ProductName,
                        ProductDescription = p.ProductDescription,
                        Price = p.Price}
                    );
        }
        private class ProductCatalogueProduct
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public Money Price { get; set; }
        }
    }

}
