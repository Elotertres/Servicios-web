namespace API.Controllers;

using api.Controllers;
using api.Entities;
using API.Data;
using API.DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;



[Authorize]
public class UsersController : BaseApiController
{

    private readonly IUserRepository _repository;

    public UsersController(UserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<MemberResponse>>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]// api/users/2
    public async Task<ActionResult<MemberResponse>> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (User == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("{username}")] // api/users/Patricio
    public async Task<ActionResult<MemberResponse>> GetByUsernameAsync(string username)
    {
        var user = await _repository.GetByUsernameAsync(username);

        if (User == null)
        {
            return NotFound();
        }

        return user;
    }
}