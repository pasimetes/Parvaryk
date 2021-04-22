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

namespace Parvaryk.Application.Commands.Entity.TransporationRequest
{
    public class ListTransporationRequestsCommand : IRequest<IEnumerable<TransporationRequestDto>>
    {
        public int OrderingId { get; set; }

        public int? TransportationRequestStatusId { get; set; }
    }

    public class ListTransporationRequestsCommandValidator : AbstractValidator<ListTransporationRequestsCommand>
    {
        public ListTransporationRequestsCommandValidator()
        {
            RuleFor(x => x.OrderingId).NotEmpty();
        }
    }

    public class ListTransporationRequestsCommandHandler : IRequestHandler<ListTransporationRequestsCommand, IEnumerable<TransporationRequestDto>>
    {
        private readonly IApplicationReadOnlyDbContext _context;
        private readonly IMapper _mapper;

        public ListTransporationRequestsCommandHandler(IApplicationReadOnlyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransporationRequestDto>> Handle(ListTransporationRequestsCommand request, CancellationToken cancellationToken)
        {
            var requests = _context.TransportationRequest.Where(x => x.OrderingId == request.OrderingId);
            if (request.TransportationRequestStatusId.HasValue)
            {
                requests = requests.Where(x => x.TransportationRequestStatusId == request.TransportationRequestStatusId.Value);
            }
            return _mapper.Map<List<TransporationRequestDto>>(await requests.ToListAsync());
        }
    }
}
