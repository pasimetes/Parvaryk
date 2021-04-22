using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Contracts.Enums;
using System.Threading.Tasks;

namespace Parvaryk.Application.DataHelpers
{
    public static class VehicleHelper
    {
        public static async Task<bool> IsVehicleNumberOccupied(string number, IApplicationDbContext context)
        {
            return await context.Vehicle.AnyAsync(x => x.Number == number);
        }

        public static async Task<bool> IsVehicleBusy(int vehicleId, IApplicationDbContext context)
        {
            return await context.Ordering.AnyAsync(
                x => x.Vehicle.VehicleId == vehicleId && (x.OrderingStatusId == (int)OrderingStatusEnum.Open || x.OrderingStatusId == (int)OrderingStatusEnum.Started)
                );
        }
    }
}
