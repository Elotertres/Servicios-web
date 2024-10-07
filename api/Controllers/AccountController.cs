using System.Security.Cryptography;
using System.Text;
using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

    public class AccountController(DataContext context) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> RegisterAsync(string UserName, string password)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser(){
            UserName = UserName,
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            passwordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
        
    }
