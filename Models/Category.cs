using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TequliasRestaurant.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-&']+$", ErrorMessage = "Category name can only contain letters, numbers, spaces, hyphens, ampersands, and apostrophes")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}