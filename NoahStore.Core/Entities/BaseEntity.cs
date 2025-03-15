using System.ComponentModel.DataAnnotations;

namespace NoahStore.Core.Entities
{
    public class BaseEntity<T>
    {
        [Required]
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
