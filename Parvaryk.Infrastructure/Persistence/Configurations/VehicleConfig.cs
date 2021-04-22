using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.VehicleId);
            builder.Property(x => x.Brand).IsRequired();
            builder.Property(x => x.Model).IsRequired();
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Country).IsRequired();

            builder.HasOne(x => x.OwnerUser)
                .WithMany(x => x.Vehicles)
                .HasForeignKey(x => x.OwnerUserId);

            builder.HasData(
                new Vehicle { VehicleId = 1, OwnerUserId = 1, Brand = "Toyota", Model = "Avensis", Year = 2004, Number = "UVN 303", Country = "Lithuania" },
                new Vehicle { VehicleId = 2, OwnerUserId = 1, Brand = "Opel", Model = "Astra", Year = 1998, Number = "FRE 867", Country = "Lithuania" }
                );
        }
    }
}
