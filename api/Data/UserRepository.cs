namespace API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using API.DataEntities;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

public class UserRepository(DataContext context,IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<AppUser>> GetAllAsync()
        => await context.Users
                .Include(u => u.Photos)
                .ToListAsync();
    public async Task<AppUser?> GetByIdAsync(int id)
        => await context.Users
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(u => u.Id == id);
    public async Task<AppUser?> GetByUsernameAsync(string username)
        => await context.Users
                .Include(u => u.Photos)
                .SingleOrDefaultAsync(u => u.UserName == username);
    public async Task<MemberResponse?> GetMemberAsync(string username) => await context.Users
            .Where(u => u.UserName == username)
            .ProjectTo<MemberResponse>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    public async Task<bool> SaveAllAsync()
        => await context.SaveChangesAsync() > 0;
    public void Update(AppUser user)
        => context.Entry(user).State = EntityState.Modified;
    public async Task<IEnumerable<MemberResponse>> GetMembersAsync() =>  await context.Users
    .ProjectTo<MemberResponse>(mapper.ConfigurationProvider)
    .ToListAsync();
    async Task<IEnumerable<AppUser>> IUserRepository.GetAllAsync() =>  await context.Users
                .Include(u => u.Photos)
                .ToListAsync();
    async Task<AppUser> IUserRepository.GetByIdAsync(int id) => await context.Users
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(u => u.Id == id);
    async Task<AppUser> IUserRepository.GetByUsernameAsync(string username) => await context.Users
                .Include(u => u.Photos)
                .SingleOrDefaultAsync(u => u.UserName == username);
}