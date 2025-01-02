namespace FlaggingService.Data;

public interface IRatingRepository
{
    Task<List<FlaggingDto>> GetFlaggings(string? date);
    Task<FlaggingDto> GetFlaggingDtoById(RequestItem requestObj);
    Task<int> FlagAnEstablishment(Rating flagEntry);
    Task DeleteFlaggingEntry(RequestItem requestObj);

}
