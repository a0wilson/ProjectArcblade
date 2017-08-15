using Microsoft.AspNetCore.Identity;

namespace ProjectArcBlade.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public PlayerDetail PlayerDetail { get; set; }
    }
}
