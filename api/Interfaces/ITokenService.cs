using api.DataEntities;
namespace api.Interfaces;
public interface ITokenService
{
        string CreateToken(AppUser user);
}