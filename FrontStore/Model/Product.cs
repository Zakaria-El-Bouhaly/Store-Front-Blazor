namespace FrontStore.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity
        {
            get; set;
        }
        public List<int> CategoryIds { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }

    public class ProductCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }

 
}
