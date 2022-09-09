using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.Commands;
using ParkyAPI.Models.Dtos;

namespace ParkyAPI.Mappers
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>();
            CreateMap<NationalPark, NationalParkCommand>();
            CreateMap<Trail, TrailDto>();
            CreateMap<Trail, TrailCommand>();
        }
    }
}
