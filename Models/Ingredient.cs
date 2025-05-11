using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TequliasRestaurant.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Ingredient name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ingredient name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-']+$",
            ErrorMessage = "Ingredient name can only contain letters, spaces, hyphens, and apostrophes")]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}