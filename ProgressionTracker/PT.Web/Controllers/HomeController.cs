using Microsoft.AspNetCore.Mvc;

namespace PT.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
