using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoahStore.Core.Entities
{
    public class ProductImage : BaseEntity<int>
    {
        [Required]
        public string ImageURL { get; set; }
        public string? AltText { get; set; }
        public string? Caption { get; set; }
        public long FileSize { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
