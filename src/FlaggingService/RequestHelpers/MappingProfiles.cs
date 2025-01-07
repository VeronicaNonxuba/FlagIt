using AutoMapper;
using Contracts;

namespace FlaggingService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rating, FlaggingDto>()
            .ForMember(dest => dest.FlagId, opt => opt.MapFrom(src => src.Flag.Id))
            .ForMember(dest => dest.EstablishmentId, opt => opt.MapFrom(src => src.Establishment.Id))
            .ForMember(dest => dest.FlaggedBy, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Flag.Color.ToString()))
            .ForMember(dest => dest.Significance, opt => opt.MapFrom(src => src.Flag.Significance.ToString()))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Establishment.Owner))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Establishment.Address))
            .ForMember(dest => dest.EstablishmentStatus, opt => opt.MapFrom(src => src.Establishment.Status.ToString()))
            .ForMember(dest => dest.EstablishmentName, opt => opt.MapFrom(src => src.Establishment.Name))
            .ForMember(dest => dest.EstablishmentTypeName, opt => opt.MapFrom(src => src.Establishment.EstType.Name));

        CreateMap<FlaggingDto, RatingCreated>();
        CreateMap<CreateFlagDto, Rating>();
        CreateMap<Rating, RatingUpdated>();
    }
}
