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

namespace Parvaryk.Application.Commands.Entity.Vehicle
{
    public class ListUserVehiclesCommand : IRequest<IEnumerable<VehicleDto>>
    {
        public int UserId { get; set; }
    }

    public class ListUserVehiclesCommandValidator : AbstractValidator<ListUserVehiclesCommand>
    {
        public ListUserVehiclesCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public class ListUserVehiclesCommandHandler : IRequestHandler<ListUserVehiclesCommand, IEnumerable<VehicleDto>>
    {
        private readonly IApplicationReadOnlyDbContext _context;
        private readonly IMapper _mapper;

        public ListUserVehiclesCommandHandler(IApplicationReadOnlyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleDto>> Handle(ListUserVehiclesCommand request, CancellationToken cancellationToken)
        {
            var vehicles = await _context.Vehicle.Where(x => x.OwnerUserId == request.UserId).ToListAsync();

            return _mapper.Map<List<VehicleDto>>(vehicles);
        }
    }
}
