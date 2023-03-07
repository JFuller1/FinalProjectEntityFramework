using Microsoft.AspNetCore.Mvc;

namespace FinalProjectEntityFramework.Controllers
{
    public class ChoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
