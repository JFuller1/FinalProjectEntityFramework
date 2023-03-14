using FinalProjectEntityFramework.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using System.Security.Cryptography.X509Certificates;

namespace FinalProjectEntityFramework.Models.ViewModels
{
    public class ViewChoresViewModel
    {
        public ICollection<Chore> Chores { get; set; } = new List<Chore>();

        public List<SelectListItem> ChoreType { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> User { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public string ChosenChoreType { get; set; }
        public string ChosenUser { get; set; }
        public string ChosenCategory { get; set; }

        public ViewChoresViewModel()
        {
            
        }

        public void PopulateVm(ApplicationDbContext _db)
        {
            Chores = _db.Chores.ToList();

            foreach (Chore chore in Chores)
            {
                if (chore.ChoreUserId != null)
                {
                    chore.ChoreUser = _db.Users.First(u => u.Id == chore.ChoreUserId);
                }

                if (chore.CategoryId != null)
                {
                    chore.Category = _db.Categories.First(c => c.Id == chore.CategoryId);
                }
            }

            ChoreType.Add(new SelectListItem { Value = "-2", Text = "Don't filter" });
            foreach (var choreType in Enum.GetNames(typeof(ChoreType)))
            {
                ChoreType.Add(new SelectListItem { Value = choreType, Text = choreType });
            }

            User.Add(new SelectListItem { Value = "-2", Text = "Don't filter" });
            User.Add(new SelectListItem { Value = "-1", Text = "Unassigned" });
            foreach (var user in _db.Users)
            {
                string fullName = user.FirstName + ' ' + user.LastName;
                User.Add(new SelectListItem { Value = user.Id, Text = fullName });
            }

            Categories.Add(new SelectListItem { Value = "-2", Text = "Don't filter" });
            Categories.Add(new SelectListItem { Value = "-1", Text = "Unassigned" });
            foreach (var category in _db.Categories)
            {
                Categories.Add(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
            }
        }
    }
}
