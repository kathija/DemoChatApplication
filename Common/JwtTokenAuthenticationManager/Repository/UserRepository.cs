namespace JwtTokenAuthenticationManager.Repository;

using ChatApplication.Db;
using ChatApplication.Model;

public class UserRepository : IUserRepository
{
    private DataContext _context;

    public UserRepository(
        DataContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.User;
    }

    public IEnumerable<User> GetById()
    {
        return _context.User;
    }

}