using FrontStore.Model;
using System.Net.Http.Json;

namespace FrontStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {

            var response = await httpClient.PostAsJsonAsync("categories", category);
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
           
            var response = await httpClient.DeleteAsync($"categories/{id}");
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Category>>("categories");
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
           
            return await httpClient.GetFromJsonAsync<Category>($"categories/{id}");
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
           var response = await httpClient.PutAsJsonAsync($"categories/{category.Id}", category);
            return await response.Content.ReadFromJsonAsync<Category>();
        }
    }
}
