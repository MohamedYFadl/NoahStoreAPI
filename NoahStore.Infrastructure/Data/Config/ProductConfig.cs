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
        }
    }
}
