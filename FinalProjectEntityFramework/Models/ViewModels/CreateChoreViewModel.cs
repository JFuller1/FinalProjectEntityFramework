using FinalProjectEntityFramework.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Office.Interop.Excel;

namespace FinalProjectEntityFramework.Models.ViewModels
{
    public class CreateChoreViewModel
    {
        public string Name { get; set; }

        public List<SelectListItem> ChoreType { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> User { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Months { get; set; } = new List<SelectListItem>();
        public ICollection<string> SelectedMonths { get; set; } = new List<string>();

        public ChoreType ChosenChoreType { get; set; }
        public string ChosenUser { get; set; }
        public string ChosenCategory { get; set; }
        public Month ChosenMonth { get; set; } = Month.Unassigned;

        public CreateChoreViewModel(ApplicationDbContext _db)
        {
            foreach (var choreType in Enum.GetNames(typeof(ChoreType)))
            {
                ChoreType.Add(new SelectListItem { Value = choreType, Text = choreType });
            }

            User.Add(new SelectListItem { Value = "-1", Text = "Unassigned" });
            foreach (var user in _db.Users)
            {
                string fullName = user.FirstName + ' ' + user.LastName;
                User.Add(new SelectListItem { Value = user.Id, Text = fullName });
            }

            Categories.Add(new SelectListItem { Value = "-1", Text = "Unassigned" });
            foreach (var category in _db.Categories)
            {
                Categories.Add(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
            }
        }

        //only needs to be called when the user selects a chore type which requires specific months inputed
        public void PopulateMonths() 
        {
            foreach (var month in Enum.GetNames(typeof(Month)))
            {
                if (month != "Unassigned")
                {
                    Months.Add(new SelectListItem { Value = month, Text = month });
                }
            }
        }

        public CreateChoreViewModel()
        {

        }
    }
}
