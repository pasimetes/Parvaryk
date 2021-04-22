using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Parvaryk.Application.DataHelpers
{
    public static class UserHelper
    {
        public static async Task<bool> IsUsernameOccupied(string username, IApplicationDbContext context)
        {
            return await context.User.AnyAsync(x => x.Username == username);
        }

        public static async Task<bool> IsEmailOccupied(string email, IApplicationDbContext context)
        {
            return await context.User.AnyAsync(x => x.UserContactInformation.Email == email);
        }
    }
}
