namespace FinalProjectEntityFramework.Models
{
    public class ChoreMonths
    {
        // Self Properties
        public int Id { get; set; }
        public Month Month { get; set; }

        // FK Properties
        public int? ChoreId { get; set; }

        // Navigation Properties
        public virtual Chore Chore { get; set; } = null!;
    }
}
