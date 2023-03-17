using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

		
		public IActionResult UsersList(int pageId = 1,string search = "")
        {
            var userList = _identityService.GetAllUsersForAdmin(pageId, search);
            return View(userList);
        }


        public IActionResult GetUserInfo(int id)
        {
            var userInfo = _identityService.GetUserInfoByUserIdForAdmin(id);
            return PartialView("_loadUserInfo", userInfo);

		}



    }
}
