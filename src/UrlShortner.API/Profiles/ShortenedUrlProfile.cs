using AutoMapper;

namespace UrlShortner.API.Profiles
{
    public class ShortenedUrlProfile : Profile
    {
        public ShortenedUrlProfile()
        {
            CreateMap<Core.Entities.ShortenedUrl, Core.Dto.ShortenedUrlModel>().ForMember(dest => dest.UrlId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
