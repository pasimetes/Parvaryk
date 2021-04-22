using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Contracts.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Entity.Ordering
{
    public class ListUserOrderingsCommand : IRequest<IEnumerable<OrderingDto>>
    {
        public int UserId { get; set; }
    }

    public class ListUserOrderingsCommandValidator : AbstractValidator<ListUserOrderingsCommand>
    {
        public ListUserOrderingsCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public class ListUserOrderingsCommandHandler : IRequestHandler<ListUserOrderingsCommand, IEnumerable<OrderingDto>>
    {
        private readonly IApplicationReadOnlyDbContext _context;
        private readonly IMapper _mapper;

        public ListUserOrderingsCommandHandler(IApplicationReadOnlyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderingDto>> Handle(ListUserOrderingsCommand request, CancellationToken cancellationToken)
        {
            var orderings = await _context.Ordering.Where(x => x.OwnerUserId == request.UserId)
                .Include(x => x.StartDirection)
                .Include(x => x.EndDirection)
                .ToListAsync();

            var orderingDtos = _mapper.Map<List<OrderingDto>>(orderings);

            foreach (var dto in orderingDtos)
            {
                dto.StartDirection = _mapper.Map<DirectionDto>(orderings.SingleOrDefault(x => x.StartDirectionId == dto.StartDirectionId).StartDirection);
                dto.EndDirection = _mapper.Map<DirectionDto>(orderings.SingleOrDefault(x => x.EndDirectionId == dto.EndDirectionId).EndDirection);
            }
            return orderingDtos;
        }
    }
}
