using NoahStore.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.ViewModels
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [StringLength(256,ErrorMessage = "You have been reached the maximum length")]
        public string Description { get; set; }
        public string SKU { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 100000)]

        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please Choose Category")]
        public int CategoryId { get; set; } 
        public DateTime Created_Date { get; set; } = DateTime.UtcNow;
        public ICollection<ProductImageDTO> Images { get; set; }
    }
}
