using System.ComponentModel.DataAnnotations;

namespace UserManagementApplication.Dtos;

public class LoginDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}