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
    public class HandleTransportationRequestCommand : IRequest<bool>
    {
        public int TransportationRequestId { get; set; }

        public bool Approved { get; set; }
    }

    public class HandleTransportationRequestCommandValidator : AbstractValidator<HandleTransportationRequestCommand>
    {
        public HandleTransportationRequestCommandValidator()
        {
            RuleFor(x => x.TransportationRequestId).NotEmpty();
            RuleFor(x => x.Approved).NotEmpty();
        }
    }

    public class HandleTransportationRequestCommandHandler : IRequestHandler<HandleTransportationRequestCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public HandleTransportationRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(HandleTransportationRequestCommand request, CancellationToken cancellationToken)
        {
            var transportationRequest = await _context.TransportationRequest.SingleOrDefaultAsync(x => x.TransportationRequestId == request.TransportationRequestId);
            if (transportationRequest.TransportationRequestStatusId != (int)TransporationRequestStatusEnum.Pending)
            {
                throw new ConflictException("Transportation request is already handled");
            }
            transportationRequest.TransportationRequestStatusId = request.Approved ? (int)TransporationRequestStatusEnum.Approved : (int)TransporationRequestStatusEnum.Rejected;
            transportationRequest.ResponseDate = DateTime.UtcNow;

            _context.TransportationRequest.Update(transportationRequest);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Approved;
        }
    }
}
