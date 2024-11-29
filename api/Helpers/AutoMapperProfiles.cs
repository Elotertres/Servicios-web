namespace API.Helpers;
using API.DataEntities;
using API.DTOs;
using AutoMapper;
using API.Extensions;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberResponse>()
            .ForMember(d => d.Age,
                o => o.MapFrom(s => s.BirthDay.CalculateAge()))
            .ForMember(d => d.PhotoUrl,
                o => o.MapFrom(s => s.Photos.FirstOrDefault(p => p.IsMain)!.Url));
        CreateMap<Photo, PhotoResponse>();
        CreateMap<MemberUpdateRequest,AppUser>();

    }
}