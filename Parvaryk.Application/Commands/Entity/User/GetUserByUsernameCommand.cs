using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Contracts.Models.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Entity.User
{
    public class GetUserByUsernameCommand : IRequest<UserDto>
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameCommandValidator : AbstractValidator<GetUserByUsernameCommand>
    {
        public GetUserByUsernameCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }

    public class GetUserByUsernameCommandHandler : IRequestHandler<GetUserByUsernameCommand, UserDto>
    {
        private readonly IApplicationReadOnlyDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByUsernameCommandHandler(IApplicationReadOnlyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUsernameCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Username == request.Username);

            return _mapper.Map<UserDto>(user);
        }
    }
}