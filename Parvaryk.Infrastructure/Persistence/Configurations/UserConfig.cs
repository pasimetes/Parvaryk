using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parvaryk.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Parvaryk.Infrastructure.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Salt).IsRequired();

            builder.HasData(new User
            {
                UserId = 1,
                Username = "super",
                Password = new byte[32] { 191, 182, 95, 176, 11, 202, 247, 14, 103, 47, 14, 101, 103, 41, 146, 100, 11, 47, 85, 198, 5, 141, 106, 81, 125, 25, 191, 1, 31, 89, 76, 196 },
                Salt = new byte[16] { 99, 66, 90, 180, 166, 213, 254, 41, 0, 84, 195, 120, 176, 116, 140, 150 },
                CreatedDate = DateTime.UtcNow
            });
        }
    }
}
