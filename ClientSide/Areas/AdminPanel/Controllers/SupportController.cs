using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;

namespace ClientSide.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Chat()
        {
            return View();
        }
    }
}
