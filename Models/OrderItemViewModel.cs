using System.ComponentModel.DataAnnotations;

namespace TequliasRestaurant.Models
{
    public class OrderItemViewModel
    {
        [Required(ErrorMessage = "Product selection is required")]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; } = 1;  // Default to 1

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal Price { get; set; }

        // Calculated property
        [Display(Name = "Total Price")]
        public decimal TotalPrice => Quantity * Price;
    }
}