using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahStore.Core.Entities;

namespace NoahStore.Infrastructure.Data.Config
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic gadgets and devices.",
                    CreatedAt = DateTime.UtcNow,
                    
                },
            new Category
            {
                Id = 2,
                Name = "Clothing",
                Description = "Apparel and fashion items.",
                CreatedAt = DateTime.UtcNow,


            },
            new Category
            {
                Id = 3,
                Name = "Home & Kitchen",
                Description = "Household items and kitchen appliances.",
                CreatedAt = DateTime.UtcNow,

            },
            new Category
            {
                Id = 4,
                Name = "Books",
                Description = "Literature and educational materials.",
                CreatedAt = DateTime.UtcNow,

            },
            new Category
            {
                Id = 5,
                Name = "Sports & Outdoors",
                Description = "Sporting goods and outdoor equipment.",
                CreatedAt = DateTime.UtcNow,

            },
            new Category
            {
                Id = 6,
                Name = "Toys & Games",
                Description = "Toys and games for all ages.",
                CreatedAt = DateTime.UtcNow,

            }

            );
        }

    }

}
