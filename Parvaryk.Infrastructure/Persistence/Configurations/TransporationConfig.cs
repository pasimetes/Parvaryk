using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class TransporationConfig : IEntityTypeConfiguration<Transportation>
    {
        public void Configure(EntityTypeBuilder<Transportation> builder)
        {
            builder.HasKey(x => x.TransportationId);
            builder.Property(x => x.OrderingId).IsRequired();
            builder.Property(x => x.CarrierUserId).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.TransportationStatusId).IsRequired();

            builder.HasOne(x => x.Ordering)
                .WithMany(x => x.Transportations)
                .HasForeignKey(x => x.OrderingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CarrierUser)
                .WithMany(x => x.Transportations)
                .HasForeignKey(x => x.CarrierUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
