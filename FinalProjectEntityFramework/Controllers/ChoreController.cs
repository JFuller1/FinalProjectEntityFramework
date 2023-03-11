using FinalProjectEntityFramework.Data;
using FinalProjectEntityFramework.Models;
using FinalProjectEntityFramework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectEntityFramework.Controllers
{
    public class ChoreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ChoreController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateChore()
        {
            return View(new CreateChoreViewModel(_db));
        }

        [HttpPost]
        public IActionResult CreateChore(CreateChoreViewModel vm)
        {
            if (vm.ChosenChoreType == ChoreType.SemiMonthly)
            {
                return RedirectToAction("SemiMonthly", vm);
            } 
            else if (vm.ChosenChoreType == ChoreType.Once || vm.ChosenChoreType == ChoreType.Annual)
            {
                return RedirectToAction("OneMonth", vm);
            } else
            {
                Chore chore = CreateBaseChore(vm);

                _db.Add(chore);
                _db.SaveChanges();
                return View(new CreateChoreViewModel(_db));
            }
        }

        public IActionResult SemiMonthly(CreateChoreViewModel vm)
        {
            vm.PopulateMonths();

            ViewBag.Months = new string[]
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult SemiMonthlySubmitted(CreateChoreViewModel vm)
        {
            Chore chore = CreateBaseChore(vm);
            chore.Months = (ICollection<Month>)vm.SelectedMonths;

            _db.Add(chore);
            _db.SaveChanges();

            return RedirectToAction("CreateChore");
        }

        public IActionResult OneMonth(CreateChoreViewModel vm)
        {
            vm.PopulateMonths();
            return View(vm);
        }

        [HttpPost]
        public IActionResult OneMonthSubmitted(CreateChoreViewModel vm)
        {
            Chore chore = CreateBaseChore(vm);
            chore.Months.Add(vm.ChosenMonth);

            _db.Add(chore);
            _db.SaveChanges();

            return RedirectToAction("CreateChore");
        }

        public Chore CreateBaseChore(CreateChoreViewModel vm)
        {
            Chore chore = new Chore();
            chore.Name = vm.Name;
            chore.IsComplete = false;
            //checking if it was left unassigned
            if (vm.ChosenUser != "-1")
            {
                chore.ChoreUserId = vm.ChosenUser;
                chore.ChoreUser = _db.Users.FirstOrDefault(u => u.Id == chore.ChoreUserId);
            }
            chore.ChoreType = vm.ChosenChoreType;
            chore.CategoryId = Int32.Parse(vm.ChosenCategory);
            chore.Category = _db.Categories.FirstOrDefault(c => c.Id == chore.CategoryId);

            return chore;
        }
    }
}
