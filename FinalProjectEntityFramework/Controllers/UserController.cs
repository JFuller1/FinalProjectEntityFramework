using FinalProjectEntityFramework.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectEntityFramework.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult CreateUser()
        {
            return View();
        }
    }
}
