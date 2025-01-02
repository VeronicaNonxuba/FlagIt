namespace FlaggingService.Data;

public interface IFlagRepository
{
    Task<Flag> GetFlagById(Guid flagId);
}
