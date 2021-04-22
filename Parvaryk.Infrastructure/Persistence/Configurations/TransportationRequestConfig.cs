using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class TransportationRequestConfig : IEntityTypeConfiguration<TransportationRequest>
    {
        public void Configure(EntityTypeBuilder<TransportationRequest> builder)
        {
            builder.HasKey(x => x.TransportationRequestId);
            builder.Property(x => x.OrderingId).IsRequired();
            builder.Property(x => x.SenderUserId).IsRequired();
            builder.Property(x => x.TransportationRequestStatusId).IsRequired();
            builder.Property(x => x.RequestDate).IsRequired();

            builder.HasOne(x => x.Ordering)
                .WithMany(x => x.TransportationRequests)
                .HasForeignKey(x => x.OrderingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.SenderUser)
                .WithMany()
                .HasForeignKey(x => x.SenderUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
