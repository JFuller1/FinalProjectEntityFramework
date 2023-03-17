using FinalProjectEntityFramework.Data;
using FinalProjectEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectEntityFramework.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category vm)
        {
            // Checks if the category is invalid
            if(vm.Name == null)
            {
                return View();
            }

            // Adds category to the database 
            Category category = new Category();
            category.Name = vm.Name;

            _db.Add(category);
            _db.SaveChanges();

            // redirects to a page which shows that the category was created successfully
            return RedirectToAction("Success", category);
        }

        public IActionResult Success(Category vm)
        {
            return View(vm);
        }
    }
}
