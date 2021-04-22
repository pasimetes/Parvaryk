using AutoMapper;
using Parvaryk.Contracts.Models.Dto;
using Parvaryk.Domain.Entities;

namespace Parvaryk.Application.Mapping
{
    public class EntityToDtoMappings : Profile
    {
        public EntityToDtoMappings()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();
            CreateMap<Ordering, OrderingDto>();
            CreateMap<OrderingDto, Ordering>();
            CreateMap<Direction, DirectionDto>();
            CreateMap<DirectionDto, Direction>();
            CreateMap<TransportationRequest, TransporationRequestDto>();
            CreateMap<TransporationRequestDto, TransportationRequest>();
        }
    }
}
