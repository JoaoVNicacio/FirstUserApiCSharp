using ApiCSharp.Models;
using ApiCSharp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiCSharp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class UserController : ControllerBase
  {
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
      _repository = repository;
    }

    // HTTP methods controllers:
    // GET:
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var users = await _repository.FindUsers();
      return users.Any() ? Ok(users) : NoContent();
    }

    // GET by ID:
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var user = await _repository.FindUserById(id);
      return user != null ? Ok(user) : NotFound("User not found!");
    }

    // POST:
    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
      _repository.AddUser(user);

      return await _repository.SaveChangesAsync() ? Ok("User added succesfully!") : BadRequest("Bad Request, couldn't save the user");
    }

    // PUT:
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, User user)
    {
      var foundUser = await _repository.FindUserById(id);

      if (user == null) return NotFound("User not found!");

      foundUser.Name = user.Name ?? foundUser.Name;
      foundUser.BirthDate = user.BirthDate != new DateTime() ? user.BirthDate : foundUser.BirthDate;

      _repository.EditUser(foundUser);

      return await _repository.SaveChangesAsync() ? Ok("User edited succesfully!") : BadRequest("Bad Request, couldn't edit the user");
    }

    // DELETE:
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var foundUser = await _repository.FindUserById(id);

      if (foundUser == null) return NotFound("User not found!");

      _repository.DeleteUser(foundUser);

      return await _repository.SaveChangesAsync() ? Ok("User removed succesfully!") : BadRequest("Bad Request, couldn't remove the user");

    }
  }
}