using System.ComponentModel.DataAnnotations;

namespace FrontStore.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }        
    }
}
