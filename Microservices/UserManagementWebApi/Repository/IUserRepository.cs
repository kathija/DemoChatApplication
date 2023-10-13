namespace UserManagementWebApi.Repository;

using AutoMapper;
using BCrypt.Net;
using ChatApplication.Model;
using UserManagementWebApi.Model;

public interface IUserRepository
{
    void Update(UpdateRequest model);
    User GetById(int id);

    IEnumerable<User> GetAll();

}

