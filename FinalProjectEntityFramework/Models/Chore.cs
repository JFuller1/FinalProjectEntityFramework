﻿using System.ComponentModel.DataAnnotations.Schema;

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

    public enum Months
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }

    public class Chore
    {
        // Self Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ChoreType ChoreType { get; set; }
        public bool IsComplete { get; set; }

        // FK Properties
        public string? ChoreUserId { get; set; }
        public int? CategoryId { get; set; }

        // Navigation Properties
        public virtual ChoreUser ChoreUser { get; set; }
        public virtual Category Category { get; set; }
        [NotMapped]
        public ICollection<Months> Months { get; set; }

    }
}