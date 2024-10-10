using Microsoft.AspNetCore.Mvc;
using TygrLabs.Application.IServices;

namespace TygrLabs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }



    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUsers()
    {
        var columns = _userService.GetColumns();
        var users = _userService.GetUsers();
        return Ok(new { Columns = columns, Users = users });
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetUsrById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
        return Ok(user);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddUser([FromBody] Dictionary<string, object> user)
    {
        try
        {
            _userService.AddUser(user);
            return Ok(new { message = "User added successfully.", Users = _userService.GetUsers() });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> EditUser(int id, [FromBody] Dictionary<string, object> updatedFields)
    {
        try
        {
            _userService.EditUser(id, updatedFields);
            return Ok(new { message = "User updated successfully.", Users = _userService.GetUsers() });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            _userService.DeleteUser(id);
            return Ok(new { message = "User deleted successfully.", Users = _userService.GetUsers() });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
