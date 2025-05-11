using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TequliasRestaurant.Models
{
    public class ProductIngredient
    {
        [Required(ErrorMessage = "Product ID is required")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Ingredient ID is required")]
        [Display(Name = "Ingredient")]
        public int IngredientId { get; set; }

        [ValidateNever]
        [ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; }

        // Additional useful fields
        [Display(Name = "Quantity")]
        [Range(0.01, 1000.00, ErrorMessage = "Quantity must be between 0.01 and 1000")]
        [Column(TypeName = "decimal(18,3)")]
        public decimal? Quantity { get; set; }

        [Display(Name = "Measurement Unit")]
        [StringLength(20, ErrorMessage = "Unit cannot exceed 20 characters")]
        public string? Unit { get; set; } // grams, ml, pieces, etc.

        [Display(Name = "Is Optional?")]
        public bool IsOptional { get; set; } = false;
    }
}