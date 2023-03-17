using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;

namespace ClientSide.Areas.AdminPanel.Controllers
{


    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]
    public class HomeController : Controller
    {
        public IActionResult Index(int pageId)
        {
            return View();
        }
    }
}
