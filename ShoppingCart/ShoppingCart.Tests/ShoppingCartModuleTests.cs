using Nancy;
using Nancy.Testing;
using Xunit;

namespace ShoppingCart.Tests
{
    public class ShoppingCartModuleTests
    {
        [Fact]
        public void GetReturnsStatusOkWhenRouteExists()
        {
            // Given
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            // When
            var result = browser.Get("/shoppingcart/1", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.OK, result.Result.StatusCode);

        }

        [Fact]
        public void GetReturnsStatusNotFoundWhenNoRouteExists()
        {
            // Given
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            // When
            var result = browser.Get("/donotexist", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.NotFound, result.Result.StatusCode);
        }

        [Fact]
        public void GetReturnsShoppingCartWhenUserExists()
        {
            // Given
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            // When
            var result = browser.Get("/shoppingcart/1", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.Equal("json", result.Result.Body);
        }

        [Fact]
        public void GetReturnsNotFoundWhenNoUserExists()
        {
            // Given
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            // When
            var result = browser.Get("/shoppingcart/123", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.Equal(HttpStatusCode.NotFound, result.Result.StatusCode);
        }
    }
}
