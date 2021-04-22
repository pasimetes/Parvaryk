using Microsoft.AspNetCore.Authentication.Cookies;
using Parvaryk.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Parvaryk.WebApi.Events
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IMemoryCacheProvider _memoryCacheProvider;
        private readonly IApplicationReadOnlyDbContext _context;

        public CustomCookieAuthenticationEvents(IMemoryCacheProvider memoryCacheProvider, IApplicationReadOnlyDbContext context)
        {
            _memoryCacheProvider = memoryCacheProvider;
            _context = context;

            OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };

            OnRedirectToAccessDenied = (context) =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        }

        //public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        //{

        //    var userId = int.Parse(context.Principal.Claims
        //        .Where(claim => claim.Type == "UserId")
        //        .Select(claim => claim.Value)
        //        .First());

        //    var cookieCreatedOn = DateTime.Parse(context.Principal.Claims
        //        .Where(claim => claim.Type == "CreatedOn")
        //        .Select(claim => claim.Value)
        //        .First());

        //    var user = (await _memoryCacheProvider.Get("Users",
        //        new Func<Task<List<User>>>(() => new Task<List<User>>(() => _context.User.ToList()))))
        //        .Single(usr => usr.UserId == userId);

        //    if (user.CredentialsLastChanged > cookieCreatedOn)
        //    {
        //        context.RejectPrincipal();
        //        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    }
        //}
    }
}
