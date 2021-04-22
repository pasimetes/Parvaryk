using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Application.DataHelpers;
using Parvaryk.Application.Helpers;
using Parvaryk.Domain.Entities;
using Parvaryk.Domain.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Business
{
    public class SignUpCommand : IRequest<int>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string HomeAdress { get; set; }

        public string HomeZipCode { get; set; }

        public string WorkAddress { get; set; }

        public string WorkZipCode { get; set; }

        public string Country { get; set; }
    }

    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.HomeAdress).NotEmpty();
            RuleFor(x => x.HomeZipCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
        }
    }

    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public SignUpCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await UserHelper.IsUsernameOccupied(request.Username, _context))
            {
                throw new ConflictException("Username is occupied!");
            }
            if (await UserHelper.IsEmailOccupied(request.Email, _context))
            {
                throw new ConflictException("Email is occupied!");
            }

            byte[] salt = PasswordHelper.GenerateRandomSalt();

            var user = new User
            {
                Username = request.Username,
                Password = PasswordHelper.HashPassword(request.Password, salt),
                Salt = salt,
                CreatedDate = DateTime.UtcNow,
                UserContactInformation = new UserContactInformation
                {
                    Email = request.Email,
                    Phone = request.Phone,
                    HomeAddress = request.HomeAdress,
                    HomeZipCode = request.HomeZipCode,
                    WorkAddress = request.WorkAddress,
                    WorkZipCode = request.WorkZipCode,
                    Country = request.Country
                }
            };

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.UserId;
        }
    }
}
