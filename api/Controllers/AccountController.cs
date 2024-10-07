using System.Security.Cryptography;
using System.Text;
using api.Data;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

    public class AccountController(DataContext context) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> RegisterAsync(RegisterRequest register)
        {
             
            using var hmac = new HMACSHA512();
            var user = new AppUser(){

            UserName = register.Username,
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
            passwordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
        private async Task<bool> UserExistAsync(string username) =>
            await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        
    }
