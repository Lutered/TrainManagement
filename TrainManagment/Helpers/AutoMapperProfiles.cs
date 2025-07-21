using AutoMapper;
using TrainManagment.Data.Entities;
using TrainManagment.DTOs;

namespace TrainManagment.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ItemDTO, Item>();
        }
    }
}
