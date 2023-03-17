using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Areas.AdminPanel.Controllers
{
	[Area(nameof(AdminPanel))]
	[PermissionChecker(1)]
	public class StoreController : Controller
	{
		private readonly IStoreService _storeService;

		public StoreController(IStoreService storeService)
        {
			_storeService = storeService;
		}
        public IActionResult SellerList(int pageId = 1 , string search = "")
		{
			var sellers = _storeService.GetAllSellersForAdmin(pageId, search);
			return View(sellers);
		}
	}
}
