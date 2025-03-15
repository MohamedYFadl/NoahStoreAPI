using System.ComponentModel.DataAnnotations.Schema;

namespace NoahStore.Core.Entities
{
    public class ProductReview : BaseEntity<int>
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        //public string UserId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        //public User User { get; set; }
    }
}
