using api.Entities;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Controllers;
[ApiController]
[Route("api/v1/[Controller]")]

public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }


    [HttpGet]
    public ActionResult <IEnumerable<AppUser>> GetUsers()
    {
        var users = _context.Users.ToList();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult <AppUser> GetUsersByid(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();
        return user;
    }
}
