using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TequliasRestaurant.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "User is required")]
        public string? UserId { get; set; }

        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, 10000.00, ErrorMessage = "Total amount must be between 0.01 and 10,000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [ValidateNever]
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}