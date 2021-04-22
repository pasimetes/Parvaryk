using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Domain.Entities;
using System.Reflection;

namespace Parvaryk.Infrastructure.Persistence
{
    public class BaseDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Direction> Direction { get; set; }
        public DbSet<Ordering> Ordering { get; set; }
        public DbSet<Transportation> Transportation { get; set; }
        public DbSet<TransportationRequest> TransportationRequest { get; set; }

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
