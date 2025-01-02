namespace FlaggingService.Data;

public interface IEstablishmentRepository
{
    Task<Establishment> GetEstablishmentById(Guid establishmentId);
    Task<EstablishmentType> GetEstablishmentTypeById(Guid establishmentTypeId);
    Task<Establishment> AddEstablishment(CreateEstablishmentDto establishment);
    Task<EstablishmentType> AddEstablishmentTYpe(CreateEstablishmentTypeDto establishment);
}
