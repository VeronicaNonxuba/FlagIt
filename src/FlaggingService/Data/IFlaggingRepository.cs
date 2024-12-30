namespace FlaggingService.Data;

public interface IFlaggingRepository
{
    Task<List<FlaggingDto>> GetFlaggings(string? date);
    Task<FlaggingDto> GetFlaggingDtoById(RequestItem requestObj);
    Task<int> FlagAnEstablishment(CreateFlagDto flagEntry);
    Task DeleteFlaggingEntry(RequestItem requestObj);
}
