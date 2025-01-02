namespace FlaggingService.Data.Users;

public interface IUsersRepository
{
    Task<User> GetUserById(Guid userId);
}
