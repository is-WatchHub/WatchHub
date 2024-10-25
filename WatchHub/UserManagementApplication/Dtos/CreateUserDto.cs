﻿using System.ComponentModel.DataAnnotations;

namespace UserManagementApplication.Dtos;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}