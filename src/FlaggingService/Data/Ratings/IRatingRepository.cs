namespace FlaggingService.Data;

public interface IRatingRepository
{
    Task<List<FlaggingDto>> GetRatings(string? date);
    Task<FlaggingDto> GetFlaggingDtoById(RequestItem requestObj);
    Task<int> FlagAnEstablishment(Rating flagEntry);
    Task<int> DeleteRatingEntry(RequestItem requestObj);

}
