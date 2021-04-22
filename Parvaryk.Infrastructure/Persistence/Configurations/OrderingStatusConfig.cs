using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class OrderingStatusConfig : IEntityTypeConfiguration<OrderingStatus>
    {
        public void Configure(EntityTypeBuilder<OrderingStatus> builder)
        {
            builder.HasKey(x => x.OrderingStatusId);
            builder.Property(x => x.Name).IsRequired();

            builder.HasData(
                new OrderingStatus { OrderingStatusId = 10, Name = "Open" },
                new OrderingStatus { OrderingStatusId = 20, Name = "Started" },
                new OrderingStatus { OrderingStatusId = 30, Name = "Finished" },
                new OrderingStatus { OrderingStatusId = 40, Name = "Failed" }
                );
        }
    }
}
