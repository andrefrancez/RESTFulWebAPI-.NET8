﻿using System.ComponentModel.DataAnnotations;

namespace VehiclesAPI.Dto;

public class RegisterDTO
{
    [Required]
    public string UserName { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
