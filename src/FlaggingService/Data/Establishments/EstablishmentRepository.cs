
namespace FlaggingService.Data.Establishments;

public class EstablishmentRepository : IEstablishmentRepository
{
    private readonly FlaggingDbContext _context;
    public EstablishmentRepository(FlaggingDbContext context)
    {
        _context = context;
    }
    public Task<Establishment> AddEstablishment(CreateEstablishmentDto establishment)
    {
        throw new NotImplementedException();
    }

    public Task<EstablishmentType> AddEstablishmentTYpe(CreateEstablishmentTypeDto establishment)
    {
        throw new NotImplementedException();
    }

    public async Task<Establishment> GetEstablishmentById(Guid establishmentId)
    {
        Establishment newEstablishment = new();
        try
        {
            var establishment = await _context.Establishments.FindAsync(establishmentId);
            if (establishment != null)
            {
                newEstablishment = establishment;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return newEstablishment;
    }

    public async Task<EstablishmentType> GetEstablishmentTypeById(Guid typeId)
    {
        EstablishmentType newType = new();
        try
        {
            var type = await _context.EstablishmentType.FindAsync(typeId);
            if (type != null)
            {
                newType = type;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return newType;
    }
}
