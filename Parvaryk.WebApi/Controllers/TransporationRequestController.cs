using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parvaryk.Application.Commands.Entity.TransporationRequest;
using Parvaryk.Contracts.Models.Dto;
using Parvaryk.Contracts.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parvaryk.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransporationRequestController : ApiController
    {
        private readonly IMapper _mapper;

        public TransporationRequestController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TransporationRequestDto>> ListTransportationRequests(int orderingId, int? transportationRequestStatusId)
        {
            return await Mediator.Send(new ListTransporationRequestsCommand { OrderingId = orderingId, TransportationRequestStatusId = transportationRequestStatusId });
        }

        [HttpPut]
        public async Task HandleTransportationRequest([FromBody] HandleTransportationRequestRequest request)
        {
            await Mediator.Send(_mapper.Map<HandleTransportationRequestCommand>(request));
        }
    }
}
