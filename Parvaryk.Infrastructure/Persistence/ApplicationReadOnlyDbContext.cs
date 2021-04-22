using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Infrastructure.Persistence
{
    public class ApplicationReadOnlyDbContext : BaseDbContext, IApplicationReadOnlyDbContext
    {
        public ApplicationReadOnlyDbContext(DbContextOptions<ApplicationReadOnlyDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("This context is read-only.");
        }
    }
}
