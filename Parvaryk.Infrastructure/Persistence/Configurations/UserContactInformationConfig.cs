using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class UserContactInformationConfig : IEntityTypeConfiguration<UserContactInformation>
    {
        public void Configure(EntityTypeBuilder<UserContactInformation> builder)
        {
            builder.HasKey(x => x.UserContactInformationId);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.HomeAddress).IsRequired();
            builder.Property(x => x.HomeZipCode).IsRequired();
            builder.Property(x => x.Country).IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserContactInformation)
                .HasForeignKey<UserContactInformation>(x => x.UserId);

            builder.HasData(new UserContactInformation
            {
                UserContactInformationId = 1,
                UserId = 1,
                Email = "laurynas.simonaitis@gmail.com",
                Phone = "+37064348984",
                HomeAddress = "localhost",
                HomeZipCode = "101011",
                Country = "Lithuania"
            });
        }
    }
}
