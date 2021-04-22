using Microsoft.AspNetCore.Http;
using Parvaryk.Contracts;
using System;
using System.Linq;
using System.Security.Claims;

namespace Parvaryk.WebApi.Services
{
    public class UserDetails : IUserDetails
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserDetails(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public int Id
        {
            get
            {
                if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new NotSupportedException("Cannot get user Id");
                }
                return int.Parse(_httpContext.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "UserId").Value);
            }
        }

        public string Username
        {
            get
            {
                if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new NotSupportedException("Cannot get user Id");
                }
                return _httpContext.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
        }
    }
}
