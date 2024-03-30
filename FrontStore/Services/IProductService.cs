using FrontStore.Model;

namespace FrontStore.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product Product);
        Task<Product> UpdateProductAsync(Product Product);
        Task<Product> DeleteProductAsync(int id);

    }
}
