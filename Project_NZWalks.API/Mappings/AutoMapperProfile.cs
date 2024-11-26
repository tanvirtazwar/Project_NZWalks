using AutoMapper;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;

namespace Project_NZWalks.API.Mappings;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
        CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
    }
}
