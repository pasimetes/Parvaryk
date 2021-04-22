using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Application.Helpers;
using Parvaryk.Domain.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Business
{
    public class SignInCommand : IRequest<ClaimsPrincipal>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, ClaimsPrincipal>
    {
        private readonly IApplicationDbContext _context;

        public SignInCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClaimsPrincipal> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Username == request.Username);
            if (user == null)
            {
                throw new NotFoundException("User was not found!");
            }

            var hashedPassword = PasswordHelper.HashPassword(request.Password, user.Salt);
            if (!hashedPassword.SequenceEqual(user.Password))
            {
                throw new InvalidCredentialsException("Specified credentials were not valid!");
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
            identity.AddClaim(new Claim("CreatedOn", DateTime.UtcNow.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

            return new ClaimsPrincipal(identity);
        }
    }
}
