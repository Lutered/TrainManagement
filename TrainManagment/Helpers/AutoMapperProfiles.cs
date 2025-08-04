using AutoMapper;
using TrainManagement.Data.Entities;
using TrainManagement.DTOs;

namespace TrainManagement.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ComponentDTO, Component>()
                .ForMember(c => c.Quantity, o => o.Condition(src => src.CanAssignQuantity));
            
            CreateMap<Component, ComponentIdDTO>();
        }
    }
}
