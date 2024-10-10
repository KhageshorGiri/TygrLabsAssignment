using Microsoft.AspNetCore.Mvc;

namespace TygrLabs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok();
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetUsrById(int id)
    {
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewUser()
    {
        return Ok();
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> UpdateUser()
    {
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        return Ok();
    }
}
