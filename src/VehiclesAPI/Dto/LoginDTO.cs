using System.ComponentModel.DataAnnotations;

namespace VehiclesAPI.Dto;

public class LoginDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
