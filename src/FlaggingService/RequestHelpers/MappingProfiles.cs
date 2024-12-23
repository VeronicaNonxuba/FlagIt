using AutoMapper;
using FlaggingService.DTOs;
using FlaggingService.Entities;

namespace FlaggingService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Flagging, FlaggingDto>()
            .IncludeMembers(x => x.Establishment)
            .IncludeMembers(y => y.Flagger)
            .IncludeMembers(z => z.Flag)
            .ForMember(
                dest => dest.EstablishmentId,
                opt => opt.MapFrom(src => src.Establishment)
            )
            .ForMember(
                dest => dest.FlagId,
                opt => opt.MapFrom(src => src.Flag)
            )
            .ForMember(
                dest => dest.FlaggedBy,
                opt => opt.MapFrom(src => src.Flagger)
            );
        CreateMap<Establishment, FlaggingDto>();
        CreateMap<Flagger, FlaggingDto>();
        CreateMap<Flag, FlaggingDto>();
    }
}
