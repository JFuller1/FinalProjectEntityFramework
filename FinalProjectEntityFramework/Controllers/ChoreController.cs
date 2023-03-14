using FinalProjectEntityFramework.Data;
using FinalProjectEntityFramework.Models;
using FinalProjectEntityFramework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

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
            if(vm.Name == null)
            {
                return View(new CreateChoreViewModel(_db));
            }

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
            if(vm.SelectedMonths.Count == 0)
            {
                return RedirectToAction("SemiMonthly", vm);
            }

            Chore chore = CreateBaseChore(vm);

            foreach (string month in vm.SelectedMonths)
            {
                ChoreMonths chosenMonth = new ChoreMonths();
                chosenMonth.Month = Enum.Parse<Month>(month);
                chosenMonth.Chore = chore;
                chosenMonth.ChoreId = chore.Id;
                _db.Add(chosenMonth);
            }

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

            ChoreMonths chosenMonth = new ChoreMonths();
            chosenMonth.Month = vm.ChosenMonth;
            chosenMonth.Chore = chore;
            chosenMonth.ChoreId = chore.Id;

            _db.Add(chosenMonth);
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
            if (vm.ChosenCategory != "-1")
            {
                chore.CategoryId = Int32.Parse(vm.ChosenCategory);
                chore.Category = _db.Categories.FirstOrDefault(c => c.Id == chore.CategoryId);
            }

            return chore;
        }

        public IActionResult Chores()
        {
            ViewChoresViewModel vm = new ViewChoresViewModel();
            vm.PopulateVm(_db);
            vm.Chores = DefaultSort();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Chores(ViewChoresViewModel vm)
        {
            vm.PopulateVm(_db);

            vm.Chores = DefaultSort();


            if(vm.ChosenUser != "-2")
            {
                if(vm.ChosenUser == "-1")
                {
                    // for unassigned
                    vm.Chores = vm.Chores.Where(chore => chore.ChoreUserId == null).ToList();
                }
                else
                {
                    // for all the normal users
                    vm.Chores = vm.Chores.Where(chore => chore.ChoreUserId == vm.ChosenUser).ToList();
                }
            }

            if (vm.ChosenCategory != "-2")
            {
                if(vm.ChosenCategory == "-1")
                {
                    vm.Chores = vm.Chores.Where(chore => chore.CategoryId == null).ToList();
                }
                else
                {
                    vm.Chores = vm.Chores.Where(chore => chore.CategoryId == int.Parse(vm.ChosenCategory)).ToList();
                }
            }

            if (vm.ChosenChoreType != "-2")
            {
                vm.Chores = vm.Chores.Where(chore => chore.ChoreType == Enum.Parse<ChoreType>(vm.ChosenChoreType)).ToList();
            }

            return View(vm);
        }

        public ICollection<Chore> DefaultSort()
        {
            // gets all the chores that do not have a month attached 
            ICollection<Chore> choresSorted = _db.Chores
                .Where(chore => (chore.ChoreType != ChoreType.Once) &&
                      (chore.ChoreType != ChoreType.Annual) &&
                      (chore.ChoreType != ChoreType.SemiMonthly))
                .OrderBy(chore => chore.ChoreType)
                .ToList();

            // gets the int value of current month
            int currentMonth = int.Parse(DateTime.Now.Month.ToString());

            // orders by months
            ICollection<ChoreMonths> choreMonths = _db.ChoreMonths.OrderBy(cm => cm.Month).ToList();

            // splits by months that have already happened and months that are still coming up
            Dictionary<bool, List<ChoreMonths>> splitMonths = choreMonths
                .GroupBy(cm => (int)cm.Month > currentMonth - 1)
                .ToDictionary(x => x.Key, x => x.ToList());

            ICollection<ChoreMonths> sortedMonths = new List<ChoreMonths>();
            // add the months that are still coming up in the year first
            sortedMonths.AddRange(splitMonths[true]);
            // then adds the months which have already happened,
            // which assumes the chore will be happening next year
            sortedMonths.AddRange(splitMonths[false]);

            // takes the closest month attached to a specific chore
            sortedMonths = sortedMonths.DistinctBy(cm => cm.ChoreId).ToList();

            foreach (ChoreMonths cm in sortedMonths)
            {
                choresSorted.Add(_db.Chores.First(chore => chore.Id == cm.ChoreId));
            }

            return choresSorted;
        }

        public IActionResult Toggle(int id)
        {
            Chore chore = _db.Chores.First(chore => chore.Id == id);
            chore.IsComplete = !chore.IsComplete;
            _db.SaveChanges();
            return RedirectToAction("Chores");
        }
    }
}
