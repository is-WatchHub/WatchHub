﻿namespace UserManagementDomain;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}