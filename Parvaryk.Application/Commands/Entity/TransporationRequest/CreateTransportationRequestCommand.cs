using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Contracts.Enums;
using Parvaryk.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Entity.TransporationRequest
{
    public class CreateTransportationRequestCommand : IRequest<int>
    {
        public int OrderingId { get; set; }

        public int SenderUserId { get; set; }
    }

    public class CreateTransportationRequestCommandValidator : AbstractValidator<CreateTransportationRequestCommand>
    {
        public CreateTransportationRequestCommandValidator()
        {
            RuleFor(x => x.OrderingId).NotEmpty();
            RuleFor(x => x.SenderUserId).NotEmpty();
        }
    }

    public class CreateTransportationRequestCommandHandler : IRequestHandler<CreateTransportationRequestCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTransportationRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTransportationRequestCommand request, CancellationToken cancellationToken)
        {
            var transportationRequest = await _context.TransportationRequest.SingleOrDefaultAsync(x
                => x.OrderingId == request.OrderingId
                && x.SenderUserId == request.SenderUserId);

            if (transportationRequest != null)
            {
                throw new ConflictException("Transporation request already exists");
            }

            transportationRequest = new Domain.Entities.TransportationRequest
            {
                OrderingId = request.OrderingId,
                SenderUserId = request.SenderUserId,
                RequestDate = DateTime.UtcNow,
                TransportationRequestStatusId = (int)TransporationRequestStatusEnum.Pending
            };

            await _context.TransportationRequest.AddAsync(transportationRequest);
            await _context.SaveChangesAsync(cancellationToken);
            return transportationRequest.TransportationRequestId;
        }
    }
}
