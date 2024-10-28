using System.Security.Cryptography;
using System.Text;
using api.Data;
using api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.DataEntities;



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
        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> LoginAsync(LoginRequest request)
        {
        var user = await context.Users.FirstOrDefaultAsync(x => 
        x.UserName.ToLower() == request.Username.ToLower());
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }
          
        using var hmac = new HMACSHA512(user.passwordSalt);
       
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
       
        for(int i = 0; i < computeHash.Length; i++){
            if(computeHash[i] != user.passwordHash[i]){
                return Unauthorized("Invalid username or password"); 
            }
        }
        return user; 
        }
        private async Task<bool> UserExistAsync(string username) =>
            await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        
    }
