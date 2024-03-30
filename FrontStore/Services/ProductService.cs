using FrontStore.Model;
using System.Net.Http.Json;

namespace FrontStore.Services
{
    public class ProductService : IProductService
    {

        public HttpClient _httpClient { get; set; }

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> CreateProductAsync(Product Product)
        {

            var response = await _httpClient.PostAsJsonAsync("Products", Product);
            return await response.Content.ReadFromJsonAsync<Product>();

        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync("Products/" + id);
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            var response = _httpClient.GetFromJsonAsync<Product>("Products/" + id);
            return response;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("Products");
        }

        public async Task<Product> UpdateProductAsync(Product Product)
        {
            var response = await _httpClient.PutAsJsonAsync("Products/" + Product.Id, Product);
            return await response.Content.ReadFromJsonAsync<Product>();
        }
    }
}
