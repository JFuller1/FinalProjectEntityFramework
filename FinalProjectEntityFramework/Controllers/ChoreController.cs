using FinalProjectEntityFramework.Data;
using FinalProjectEntityFramework.Models;
using FinalProjectEntityFramework.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            // Only checks the name because all of the drop downs will contain data by defaul
            if(vm.Name == null)
            {
                return View(new CreateChoreViewModel(_db));
            }

            // Checks which page to redirect to if months need to be selected
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
                return RedirectToAction("Success", chore);
            }
        }

        public IActionResult Success(Chore vm)
        {
            return View(vm);
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
            // Checks if no months were selected
            if(vm.SelectedMonths.Count == 0)
            {
                return RedirectToAction("SemiMonthly", vm);
            }

            Chore chore = CreateBaseChore(vm);

            // Iterates through all the selected months and adds to database
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

            return RedirectToAction("Success", chore);
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

        [AllowAnonymous]
        public IActionResult Chores(string filter1, string filter2, string filter3)
        {
            ViewChoresViewModel vm = new ViewChoresViewModel();
            vm.PopulateVm(_db);
            // Performs the default sort
            vm.Chores = DefaultSort();

            // Checks the url to see if there are any filters to apply
            vm.Chores = FilterByURL(vm.Chores, filter1, filter2, filter3);

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Chores(ViewChoresViewModel vm, string filter1, string filter2, string filter3)
        {
            vm.PopulateVm(_db);

            vm.Chores = DefaultSort();

            // The following if statements check all the dropdowns and filter the data accordingly

            // -2 is the value connected with don't filter
            if(vm.ChosenUser != "-2")
            {
                if(vm.ChosenUser == "-1")
                {
                    // shows all unassigned
                    vm.Chores = vm.Chores.Where(chore => chore.ChoreUserId == null).ToList();
                }
                else
                {
                    // filters for specified users (chores that have users which are not unassigned)
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

            if (vm.ChosenCompletionFilter != "-2")
            {
                if (vm.ChosenCompletionFilter == "2")
                {
                    vm.Chores = vm.Chores.Where(chore => chore.IsComplete == true).ToList();
                }
                else
                {
                    vm.Chores = vm.Chores.Where(chore => chore.IsComplete == false).ToList();
                }
            }

            // After filtering the data also checks if there are other filters in the url
            vm.Chores = FilterByURL(vm.Chores, filter1, filter2, filter3);

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult ChoreDetails(int id)
        {
            Chore chore = _db.Chores.First(chore => chore.Id == id);

            chore.Category = _db.Categories.FirstOrDefault(category =>  category.Id == chore.CategoryId);
            chore.ChoreUser = _db.Users.FirstOrDefault(user => user.Id == chore.ChoreUserId);

            var choreMonths = _db.ChoreMonths.Where(cm => cm.ChoreId == id).ToList();

            foreach(var choreMonth in choreMonths)
            {
                chore.ChoreMonths.Add(choreMonth);
            }

            // It was adding the months twice, this was my fix
            chore.ChoreMonths = chore.ChoreMonths.DistinctBy(cm => cm.Month).ToList();

            return View(chore);
        }

        // TOGGLES 
        public IActionResult ToggleOnDetails(int id)
        {
            // finds the chore and changes isComplete to the opposite value (true -> false) (false -> true)
            Chore chore = _db.Chores.First(chore => chore.Id == id);
            chore.IsComplete = !chore.IsComplete;
            _db.SaveChanges();
            return RedirectToAction("ChoreDetails", new { id = id });
        }
        public IActionResult Toggle(int id)
        {
            Chore chore = _db.Chores.First(chore => chore.Id == id);
            chore.IsComplete = !chore.IsComplete;
            _db.SaveChanges();
            return RedirectToAction("Chores");
        }

        //HELPER FUNCTIONS

        public Chore CreateBaseChore(CreateChoreViewModel vm)
        {
            //Assigns all the universal data to a chore (which is everything except for months)

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
                .GroupBy(cm => (int)cm.Month >= currentMonth - 1)
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

        public ICollection<Chore> FilterByURL(ICollection<Chore> chores, string filter1, string filter2, string filter3)
        {
            bool userFiltering = false;
            bool monthFiltering = false;
            bool categoryFiltering = false;

            // Checks if the filter is in the url
            if (filter1 != null)
            {
                // If it is, checks if the filter is a valid user and if user hasent already been filtered
                if(IsUserFilter(filter1) && userFiltering == false)
                {
                    userFiltering = true;
                    chores = FilterByUser(chores, filter1);
                } // Same as the one above but for months
                else if(IsMonthFilter(filter1) && monthFiltering == false)
                {
                    monthFiltering = true;
                    chores = FilterByMonth(chores, filter1);
                } // And then for category
                else if(IsCategoryFilter(filter1) && categoryFiltering == false)
                {
                    categoryFiltering = true;
                    chores = FilterByCategory(chores, filter1);
                }
            }

            if (filter2 != null)
            {
                if (IsUserFilter(filter2) && userFiltering == false)
                {
                    userFiltering = true;
                    chores = FilterByUser(chores, filter2);
                }
                else if (IsMonthFilter(filter2) && monthFiltering == false)
                {
                    monthFiltering = true;
                    chores = FilterByMonth(chores, filter2);
                }
                else if (IsCategoryFilter(filter2) && categoryFiltering == false)
                {
                    categoryFiltering = true;
                    chores = FilterByCategory(chores, filter2);
                }
            }


            // no longer need to set the bools because they will not be reference again in this function
            if (filter3 != null)
            {
                if (IsUserFilter(filter3) && userFiltering == false)
                {
                    chores = FilterByUser(chores, filter3);
                }
                else if (IsMonthFilter(filter3) && monthFiltering == false)
                {
                    chores = FilterByMonth(chores, filter3);
                }
                else if (IsCategoryFilter(filter3) && categoryFiltering == false)
                {
                    chores = FilterByCategory(chores, filter3);
                }
            }

            return chores;
        }

        public bool IsUserFilter(string filter)
        {
            filter = filter.ToLower();

            // Checks if the url contains the users firstname, lastname, full name, or unassigned
            foreach(var user in _db.Users)
            {
                if(user.FirstName.ToLower() == filter)
                {
                    return true;
                } 
                else if(user.LastName.ToLower() == filter)
                {
                    return true;
                } 
                else if($"{user.FirstName.ToLower()} {user.LastName.ToLower()}" == filter)
                {
                    return true;
                }
                else if(filter == "unassigned")
                {
                    return true;
                }
            }

            return false;
        }

        public ICollection<Chore> FilterByUser(ICollection<Chore> chores, string filter)
        {
            foreach(var chore in chores)
            {
                // checks if its a "real" (not unassigned) user or not, if so assigns to choreuser,
                // if not creates a new chore user with the name of unassigned
                if(chore.ChoreUserId != null)
                {
                    chore.ChoreUser = _db.Users.First(user => user.Id == chore.ChoreUserId);
                } else
                {
                    chore.ChoreUser = new ChoreUser();
                    chore.ChoreUser.FirstName = "Unassigned";
                    chore.ChoreUser.LastName = "";
                }
            }

            chores = chores.Where(
                chore => chore.ChoreUser.FirstName.ToLower() == filter.ToLower() ||
                chore.ChoreUser.LastName.ToLower() == filter.ToLower() || 
                chore.ChoreUser.FirstName.ToLower() + " " + chore.ChoreUser.LastName.ToLower() == filter.ToLower()
             ).ToList();

            return chores;
        }

        // The following are the same idea as the previous 2, just for category
        public bool IsCategoryFilter(string filter)
        {
            filter = filter.ToLower();

            foreach(var category in _db.Categories)
            {
                if(category.Name.ToLower() == filter)
                {
                    return true;
                }
                else if (filter == "unassigned")
                {
                    return true;
                }
            }

            return false;
        }

        public ICollection<Chore> FilterByCategory(ICollection<Chore> chores, string filter)
        {
            foreach (var chore in chores)
            {
                if (chore.CategoryId != null)
                {
                    chore.Category = _db.Categories.First(category => category.Id == chore.CategoryId);
                }
                else
                {
                    chore.Category = new Category();
                    chore.Category.Name = "Unassigned";
                }
            }

            chores = chores.Where(chore => chore.Category.Name.ToLower() == filter.ToLower()).ToList();

            return chores;
        }

        public bool IsMonthFilter(string filter)
        {
            filter = filter.ToLower();

            // Has the != unassigned so it doesnt try and filter months that are unassigned

            foreach(var month in Enum.GetNames(typeof(Month)))
            {
                if(month.ToLower() == filter && filter != "unassigned")
                {
                    return true;
                }
            }

            return false;
        }

        public ICollection<Chore> FilterByMonth(ICollection<Chore> chores, string filter)
        {
            foreach(Chore chore in chores)
            {
                chore.ChoreMonths = _db.ChoreMonths.Where(cm => cm.ChoreId == chore.Id && cm.Month == Enum.Parse<Month>(filter)).ToList();
            }

            chores = chores.Where(chore => chore.ChoreMonths.Count == 1).ToList();

            return chores;
        }
    }
}
