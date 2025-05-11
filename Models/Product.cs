using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace TequliasRestaurant.Models
{
    public class Product
    {
        public Product()
        {
            ProductIngredients = new List<ProductIngredient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2-100 characters")]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, 10000, ErrorMessage = "Stock must be between 0-10000")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [NotMapped]
        [Display(Name = "Product Image")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Only image files are allowed")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum file size is 5MB")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; }

        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients { get; set; }
    }

    // Custom attribute for file size validation
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}