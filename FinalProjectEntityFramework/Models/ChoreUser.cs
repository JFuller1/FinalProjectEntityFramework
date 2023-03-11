using Microsoft.AspNetCore.Identity;

namespace FinalProjectEntityFramework.Models
{
    public class ChoreUser : IdentityUser
    {
        // Self Properties
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
