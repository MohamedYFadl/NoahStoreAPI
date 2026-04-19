using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Infrastructure.Data.Config
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(u=>u.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.SKU).HasMaxLength(12);
            //builder.HasData(
            //    new Product
            //    {
            //        Id = 1,
            //        Name = "Laptop",
            //        Description = "High-performance laptop",
            //        Price = 1200.00m,
            //        SKU = "EL-LP-001",
            //        StockQuantity = 50,
            //        IsAvailaible = true,
            //        CreatedAt = DateTime.UtcNow,
            //        CategoryId = 1
            //    },
            //        new Product
            //        {
            //            Id = 2,
            //            Name = "Smartphone",
            //            Description = "Latest smartphone model",
            //            Price = 800.00m,
            //            SKU = "EL-SP-002",
            //            StockQuantity = 100,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //            CategoryId = 1
            //        },
            //        new Product
            //        {
            //            Id = 3,
            //            Name = "T-Shirt",
            //            Description = "Cotton t-shirt",
            //            Price = 20.00m,
            //            SKU = "CL-TS-001",
            //            StockQuantity = 200,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //            CategoryId = 2
            //        }, new Product
            //        {
            //            Id = 4,
            //            Name = "Blender",
            //            Description = "High-speed blender",
            //            Price = 80.00m,
            //            SKU = "HK-BL-001",
            //            StockQuantity = 30,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //            CategoryId = 3,


                        
            //        },
            //        new Product
            //        {
            //            Id = 5,
            //            Name = "Pillows",
            //            Description = "Set of comfortable pillows",
            //            Price = 35.00m,
            //            SKU = "HK-PI-002",
            //            StockQuantity = 100,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //            CategoryId = 3,
            //        }, new Product
            //        {
            //            CategoryId = 4,
            //            Id = 6,
            //            Name = "Novel",
            //            Description = "Fiction novel",
            //            Price = 15.00m,
            //            SKU = "BK-NV-001",
            //            StockQuantity = 150,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //        },
            //        new Product
            //        {
            //            CategoryId = 4,
            //            Id = 7,
            //            Name = "Textbook",
            //            Description = "Educational textbook",
            //            Price = 40.00m,
            //            SKU = "BK-TB-002",
            //            StockQuantity = 50,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //        },
            //        new Product
            //        { 
            //            CategoryId = 5,
            //            Id = 8,
            //            Name = "Basketball",
            //            Description = "Official size basketball",
            //            Price = 25.00m,
            //            SKU = "SO-BB-001",
            //            StockQuantity = 80,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //        },
            //        new Product
            //        {
            //            CategoryId = 5,
            //            Id = 9,
            //            Name = "Tent",
            //            Description = "Camping tent for 4 people",
            //            Price = 150.00m,
            //            SKU = "SO-TT-002",
            //            StockQuantity = 20,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //         },
            //         new Product 
            //         { CategoryId = 6,
            //             Id = 10,
            //             Name = "Board Game",
            //             Description = "Strategy board game",
            //             Price = 30.00m,
            //             SKU = "TG-BG-001",
            //             StockQuantity = 60,
            //             IsAvailaible = true,
            //             CreatedAt = DateTime.UtcNow,
            //             },
            //        new Product
            //        {
            //            CategoryId = 6,
            //            Id = 11,
            //            Name = "Action Figure",
            //            Description = "Collectible action figure",
            //            Price = 18.00m,
            //            SKU = "TG-AF-002",
            //            StockQuantity = 120,
            //            IsAvailaible = true,
            //            CreatedAt = DateTime.UtcNow,
            //         });
        }
    }
}
