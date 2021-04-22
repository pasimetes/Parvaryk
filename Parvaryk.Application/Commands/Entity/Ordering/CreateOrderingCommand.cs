using AutoMapper;
using FluentValidation;
using MediatR;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Application.DataHelpers;
using Parvaryk.Contracts.Models.Dto;
using Parvaryk.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Entity.Ordering
{
    public class CreateOrderingCommand : IRequest<int>
    {
        public int OwnerUserId { get; set; }

        public int VehicleId { get; set; }

        public DirectionDto StartDirection { get; set; }

        public DirectionDto EndDirection { get; set; }
    }

    public class CreateProjectCommandValidator : AbstractValidator<CreateOrderingCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.OwnerUserId).NotEmpty();
            RuleFor(x => x.VehicleId).NotEmpty();

            RuleFor(x => x.StartDirection).NotEmpty();
            RuleFor(x => x.StartDirection.Address).NotEmpty();
            RuleFor(x => x.StartDirection.City).NotEmpty();
            RuleFor(x => x.StartDirection.Country).NotEmpty();
            RuleFor(x => x.StartDirection.Latitude).NotEmpty();
            RuleFor(x => x.StartDirection.Longtitude).NotEmpty();

            RuleFor(x => x.EndDirection).NotEmpty();
            RuleFor(x => x.EndDirection.Address).NotEmpty();
            RuleFor(x => x.EndDirection.City).NotEmpty();
            RuleFor(x => x.EndDirection.Country).NotEmpty();
            RuleFor(x => x.EndDirection.Latitude).NotEmpty();
            RuleFor(x => x.EndDirection.Longtitude).NotEmpty();
        }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateOrderingCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            if (await VehicleHelper.IsVehicleBusy(request.VehicleId, _context))
            {
                throw new ConflictException("Vehicle is currently participating in another project");
            }

            var project = new Domain.Entities.Ordering
            {
                OwnerUserId = request.OwnerUserId,
                VehicleId = request.VehicleId,
                OrderingStatusId = 10,
                CreatedDate = DateTime.UtcNow,
                StartDirection = _mapper.Map<Domain.Entities.Direction>(request.StartDirection),
                EndDirection = _mapper.Map<Domain.Entities.Direction>(request.EndDirection)
            };

            await _context.Ordering.AddAsync(project);
            await _context.SaveChangesAsync(cancellationToken);
            return project.OrderingId;
        }
    }
}
