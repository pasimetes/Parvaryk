using Microsoft.EntityFrameworkCore;
using Parvaryk.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> User { get; }
        DbSet<Vehicle> Vehicle { get; }
        DbSet<Direction> Direction { get; }
        DbSet<Ordering> Ordering { get; }
        DbSet<Transportation> Transportation { get; }
        DbSet<TransportationRequest> TransportationRequest { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
