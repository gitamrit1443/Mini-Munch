using MiniMunch.Web.Models;

namespace MiniMunch.Web.Services;

public interface IJwtService
{
    string GenerateAccessToken(AppUser user);
    DateTime GetAccessTokenExpiryUtc();
}
