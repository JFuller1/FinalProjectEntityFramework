namespace FinalProjectEntityFramework.Models
{
    public class Category
    {
        // Self Properties
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public virtual ICollection<Chore> Chores { get; set; }
    }
}
