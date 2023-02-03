using Microsoft.AspNetCore.Mvc;

namespace SMS.Controllers
{
    public class UniversitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
