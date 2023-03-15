using Microsoft.Office.Interop.Excel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public enum Month
    {
        [Display(Name = "Unassigned")]
        Unassigned = -1,

        [Display(Name = "January")]
        January,

        [Display(Name = "February")]
        February,

        [Display(Name = "March")]
        March,

        [Display(Name = "April")]
        April,

        [Display(Name = "May")]
        May,

        [Display(Name = "June")]
        June,

        [Display(Name = "July")]
        July,

        [Display(Name = "August")]
        August,

        [Display(Name = "September")]
        September,

        [Display(Name = "October")]
        October,

        [Display(Name = "November")]
        November,

        [Display(Name = "December")]
        December,
    }

    public class Chore
    {
        // Self Properties
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ChoreType ChoreType { get; set; }
        public bool IsComplete { get; set; }

        // FK Properties
        public string? ChoreUserId { get; set; }
        public int? CategoryId { get; set; }

        // Navigation Properties
        public virtual ChoreUser? ChoreUser { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<ChoreMonths> ChoreMonths { get; set; } = new List<ChoreMonths>();
    }
}
