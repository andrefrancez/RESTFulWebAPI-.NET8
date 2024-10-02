using Microsoft.AspNetCore.Identity;

namespace VehiclesAPI.Models;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
