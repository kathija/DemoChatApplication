namespace UserManagementWebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementWebApi.Model;
using UserManagementWebApi.Repository;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserRepository _userRepository;

    public UsersController(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userRepository.GetAll();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = _userRepository.GetById(id);
        return Ok(user);
    }

    [Authorize]
    [HttpPut]
    public IActionResult Update(UpdateRequest model)
    {
        _userRepository.Update(model);
        return Ok(new { message = "User updated successfully" });
    }
}