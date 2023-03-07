using Microsoft.AspNetCore.Identity;

namespace FinalProjectEntityFramework.Models
{
    public class ChoreUser : IdentityUser
    {
        // Self Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation Properties
        public virtual ICollection<Chore> Chores { get; set; }

    }
}
