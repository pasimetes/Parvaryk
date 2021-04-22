using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parvaryk.Application.Commands.Entity.Ordering;
using Parvaryk.Contracts;
using Parvaryk.Contracts.Models.Dto;
using Parvaryk.Contracts.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parvaryk.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderingController : ApiController
    {
        private readonly IUserDetails _userDetails;
        private readonly IMapper _mapper;

        public OrderingController(IUserDetails userDetails, IMapper mapper)
        {
            _userDetails = userDetails;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<int> CreateOrdering([FromBody] CreateOrderingRequest request)
        {
            return await Mediator.Send(_mapper.Map<CreateOrderingCommand>(request));
        }

        [HttpGet]
        public async Task<IEnumerable<OrderingDto>> ListUserOrderings(int userId)
        {
            return await Mediator.Send(new ListUserOrderingsCommand { UserId = userId });
        }
    }
}
