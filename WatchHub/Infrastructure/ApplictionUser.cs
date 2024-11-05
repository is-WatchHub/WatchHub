using Microsoft.AspNetCore.Identity;
using UserManagementDomain;

namespace Infrastructure
{
    public class ApplictionUser : IdentityUser
    {
        public Role Role { get; set; }
    }
}
