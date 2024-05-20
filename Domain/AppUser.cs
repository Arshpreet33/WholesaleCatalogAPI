using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser: IdentityUser
    {
        public required string DisplayName { get; set; }
        public string? Bio { get; set; }
        public required string Role { get; set; }
        public required bool IsActive { get; set; }
    }
}
