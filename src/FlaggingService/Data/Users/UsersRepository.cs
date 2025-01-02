namespace FlaggingService.Data.Users;

public class UsersRepository : IUsersRepository
{
    private readonly FlaggingDbContext _context;

    public UsersRepository(FlaggingDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserById(Guid userId)
    {
        User newUser = new();
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                newUser = user;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return newUser;
    }
}

