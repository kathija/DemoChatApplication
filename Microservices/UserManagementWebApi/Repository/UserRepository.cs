namespace UserManagementWebApi.Repository;

using AutoMapper;
using BCrypt.Net;
using ChatApplication.Db;
using ChatApplication.Model;
using UserManagementWebApi.Helpers;
using UserManagementWebApi.Model;

public class UserRepository : IUserRepository
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.User;
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public void Update(UpdateRequest model)
    {
        var user = getUser(model.Id);

        // validate
        if (model.Username != user.UserName && _context.User.Any(x => x.UserName == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.Password = BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.User.Update(user);
        _context.SaveChanges();
        // _context.SaveChanges();
    }

    private User getUser(int id)
    {
        var user = _context.User.Find(id);
        return user ?? throw new KeyNotFoundException("User not found");
    }
}