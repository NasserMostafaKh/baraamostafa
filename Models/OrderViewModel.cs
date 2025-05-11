using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace TequliasRestaurant.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be at least 0.01")]
        [DataType(DataType.Currency)]
        [Display(Name = "Order Total")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Order must contain at least one item")]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        [Display(Name = "Order Items")]
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

        [ValidateNever] // Prevents over-posting attacks
        public IEnumerable<Product> Products { get; set; }

        // Additional useful properties for order processing
        [Display(Name = "Customer Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? CustomerNotes { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = "Cash"; // Default value

        [Display(Name = "Delivery Address")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? DeliveryAddress { get; set; }
    }
}