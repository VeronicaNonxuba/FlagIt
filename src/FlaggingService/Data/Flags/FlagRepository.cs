
namespace FlaggingService.Data.Flags;

public class FlagRepository : IFlagRepository
{
    private readonly FlaggingDbContext _context;

    public FlagRepository(FlaggingDbContext context)
    {
        _context = context;
    }

    public async Task<Flag> GetFlagById(Guid flagId)
    {
        Flag newFlag = new();
        try
        {
            var flag = await _context.Flags.FindAsync(flagId);
            if (flag != null)
            {
                newFlag = flag;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return newFlag;
    }
}
