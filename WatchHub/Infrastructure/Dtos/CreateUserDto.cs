﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class CreateUserDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}