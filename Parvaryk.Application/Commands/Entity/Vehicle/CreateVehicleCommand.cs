using FluentValidation;
using MediatR;
using Parvaryk.Application.Common.Interfaces;
using Parvaryk.Application.DataHelpers;
using Parvaryk.Domain.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Parvaryk.Application.Commands.Entity.Vehicle
{
    public class CreateVehicleCommand : IRequest<int>
    {
        public int VehicleId { get; set; }

        public int OwnerUserId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Number { get; set; }

        public string Country { get; set; }
    }

    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.OwnerUserId).NotEmpty();
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Year).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
        }
    }

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateVehicleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (await VehicleHelper.IsVehicleNumberOccupied(request.Number, _context))
            {
                throw new ConflictException("Vehicle number is occupied!");
            }

            var vehicle = new Domain.Entities.Vehicle
            {
                OwnerUserId = request.OwnerUserId,
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                Number = request.Number,
                Country = request.Country
            };

            await _context.Vehicle.AddAsync(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return vehicle.VehicleId;
        }
    }
}
