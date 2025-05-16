using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahStore.Core.Entities.Order;

namespace NoahStore.Infrastructure.Data.Config
{
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                new DeliveryMethod { Id = 1,  DeliveryTime = "15 Days", Description = "Fastest way", Price = 15, Name = "DHL", IsDeleted = false,CreatedAt = new DateTime(2023, 1, 1) },
                                new DeliveryMethod { Id = 2,  DeliveryTime = "12 Days", Description = "Save way", Price = 10, Name = "DHL", IsDeleted = false, CreatedAt = new DateTime(2023, 1, 1) }

                );

        }
    }
}
