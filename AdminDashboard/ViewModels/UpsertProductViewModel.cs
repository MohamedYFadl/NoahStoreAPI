using Microsoft.AspNetCore.Mvc.Rendering;
using NoahStore.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.ViewModels
{
    public class UpsertProductViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [StringLength(256, ErrorMessage = "You have been reached the maximum length")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 100000)]

        public decimal Price { get; set; }
        [Required]
        [Range(1,1000)]
        public int StockQuantity { get; set; }
        [Required(ErrorMessage = "Please Choose Category")]
        public int CategoryId { get; set; }
        public Category?  Category { get; set; }
        public DateTime Created_Date { get; set; } = DateTime.UtcNow;
        public IFormFileCollection Images { get; set; }

        //public SelectList Categories { get; set; }
    }
}
