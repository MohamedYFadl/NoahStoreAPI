using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahStore.Core.Entities.Order;

namespace NoahStore.Infrastructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.OrderStatus)
                .HasConversion(o => o.ToString()
                , o =>(OrderStatus) Enum.Parse(typeof(OrderStatus), o));
            builder.Property(o => o.PaymentStatus)
               .HasConversion(o => o.ToString()
               , o => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), o));
            builder.OwnsOne(o => o.shippingAddress, n => { n.WithOwner(); });

            builder.HasMany(o => o.OrderItems).WithOne();



        }
    }
}
