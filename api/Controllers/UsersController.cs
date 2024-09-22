using api.Entities;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult <IEnumerable<AppUser>>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUsersByidAsync(int id)
    {
        var user =await  _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpGet("{name}")]
    public ActionResult <string> Ready(string name)
    {
        
        return $"Hola: {name}";
    }
}
