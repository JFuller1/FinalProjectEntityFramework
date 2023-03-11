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
            Category category = new Category();
            category.Name = vm.Name;

            _db.Add(category);
            _db.SaveChanges();

            return View();
        }
    }
}
