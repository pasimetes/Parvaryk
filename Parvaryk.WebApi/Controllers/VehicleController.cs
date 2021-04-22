using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parvaryk.Application.Commands.Entity.Vehicle;
using Parvaryk.Contracts.Models.Dto;
using Parvaryk.Contracts.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parvaryk.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ApiController
    {
        private readonly IMapper _mapper;

        public VehicleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleDto>> ListUserVehicles(int userId)
        {
            return await Mediator.Send(new ListUserVehiclesCommand { UserId = userId });
        }

        [HttpPost]
        public async Task<int> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            return await Mediator.Send(_mapper.Map<CreateVehicleCommand>(request));
        }
    }
}
