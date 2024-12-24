using AutoMapper;
using FlaggingService.DTOs;
using FlaggingService.Entities;

namespace FlaggingService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Flagging, FlaggingDto>()
            .ForMember(dest => dest.FlagId, opt => opt.MapFrom(src => src.Flag.Id))
            .ForMember(dest => dest.EstablishmentId, opt => opt.MapFrom(src => src.Establishment.Id))
            .ForMember(dest => dest.FlaggedBy, opt => opt.MapFrom(src => src.Flagger.Id))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Flag.Color))
            .ForMember(dest => dest.Significance, opt => opt.MapFrom(src => src.Flag.Significance))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Establishment.Owner))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Establishment.Address))
            .ForMember(dest => dest.EstablishmentStatus, opt => opt.MapFrom(src => src.Establishment.Status))
            .ForMember(dest => dest.EstablishmentStatusString, opt => opt.MapFrom(src => src.Establishment.Status.ToString()))
            .ForMember(dest => dest.EstablishmentName, opt => opt.MapFrom(src => src.Establishment.Name))
            .ForMember(dest => dest.EstablishmentTypeName, opt => opt.MapFrom(src => src.Establishment.EstType.Name));
    }
}
