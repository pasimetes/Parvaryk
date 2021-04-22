using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class DirectionConfig : IEntityTypeConfiguration<Direction>
    {
        public void Configure(EntityTypeBuilder<Direction> builder)
        {
            builder.HasKey(x => x.DirectionId);
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longtitude).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Country).IsRequired();
        }
    }
}
