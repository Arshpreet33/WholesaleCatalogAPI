using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser: IdentityUser
    {
        public required string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? Role { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
