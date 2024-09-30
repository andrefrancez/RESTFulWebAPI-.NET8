using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace VehiclesAPI.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GetJwtAccessToken(IEnumerable<Claim> claims, IConfiguration _config);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
}