using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class OrderingConfig : IEntityTypeConfiguration<Ordering>
    {
        public void Configure(EntityTypeBuilder<Ordering> builder)
        {
            builder.HasKey(x => x.OrderingId);
            builder.Property(x => x.OwnerUserId).IsRequired();
            builder.Property(x => x.VehicleId).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.StartDirectionId).IsRequired();
            builder.Property(x => x.EndDirectionId).IsRequired();
            builder.Property(x => x.OrderingStatusId).IsRequired();

            builder.HasOne(x => x.OwnerUser)
                .WithMany(x => x.Orderings)
                .HasForeignKey(x => x.OwnerUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.Orderings)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.StartDirection)
                .WithMany()
                .HasForeignKey(x => x.StartDirectionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.EndDirection)
                .WithMany()
                .HasForeignKey(x => x.EndDirectionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
