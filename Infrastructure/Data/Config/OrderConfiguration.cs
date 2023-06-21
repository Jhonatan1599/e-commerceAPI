using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {   
            // configuring a one-to-one relationship where the owned entity (in this case, ShipToAddress) is completely dependent on the owning entity.
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {   
                //configuring the ownership relationship by specifying that the ShipToAddress entity is owned by the owning entity.
                a.WithOwner();
            });
            builder.Navigation(a => a.ShipToAddress).IsRequired();
            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)
                );
            //if order is deleted, the related OrderItems are deleted
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}