using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TequliasRestaurant.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }

        [ValidateNever]
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Calculated property for total price (optional)
        [NotMapped]
        [Display(Name = "Total Price")]
        public decimal TotalPrice => Quantity * Price;
    }
}