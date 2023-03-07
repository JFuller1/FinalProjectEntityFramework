namespace FinalProjectEntityFramework.Models
{
    public enum ChoreType
    {
        Once,
        Daily,
        Weekly,
        Monthly,
        SemiMonthly,
        Annual
    }

    public class Chore
    {
        // Self Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ChoreType ChoreType { get; set; }
        public bool IsComplete { get; set; }

        // Optional because only semiMonthly and Annual will need a month defined
        public string? Month { get; set; }  

        // FK Properties
        public string? ChoreUserId { get; set; }
        public int? CategoryId { get; set; }

        // Navigation Properties
        public virtual ChoreUser ChoreUser { get; set; }
        public virtual Category Category { get; set; }

    }
}
