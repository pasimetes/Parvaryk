using AutoMapper;
using Parvaryk.Application.Commands.Business;
using Parvaryk.Application.Commands.Entity.Ordering;
using Parvaryk.Application.Commands.Entity.TransporationRequest;
using Parvaryk.Application.Commands.Entity.Vehicle;
using Parvaryk.Contracts.Models.Request;

namespace Parvaryk.Application.Mapping
{
    public class RequestToCommandMappings : Profile
    {
        public RequestToCommandMappings()
        {
            CreateMap<SignInRequest, SignInCommand>();
            CreateMap<SignUpRequest, SignUpCommand>();
            CreateMap<CreateVehicleRequest, CreateVehicleCommand>();
            CreateMap<CreateOrderingRequest, CreateOrderingCommand>();
            CreateMap<HandleTransportationRequestRequest, HandleTransportationRequestCommand>();
        }
    }
}
