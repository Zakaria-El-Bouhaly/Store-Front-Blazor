using System.ComponentModel.DataAnnotations;

namespace FrontStore.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name is too short.")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Description is too short.")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000.")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int Quantity { get; set; }        
        public List<int> CategoryIds { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Category> Categories { get; set; }
    }
 
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }


}
