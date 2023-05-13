using DataLayer.Models.Store;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.StoreViewModels;

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

		public IActionResult UpdateSeller(int id)
		{
			var seller = _storeService.GetSellerForUpdate(id);
			return View(seller);
		}

		[HttpPost]
		public IActionResult UpdateSeller(ManageSellerViewModel model)
		{
		 bool res =	_storeService.UpdateSellerRequest(model);
			if(res)
			{
				TempData["success"] = "ویرایش با موفقیت انجام شد";
				return RedirectToAction("SellerList");
			}
			TempData["error"] = "عملیات با شکست مواجه شد";
			return View(model);
		}



        public IActionResult CategoryList()
        {
            var categories = _storeService.GetCategoriesForAdmin();
            return View(categories);
        }


		public IActionResult CreateCategory()
		{
			ViewBag.ParentCategories = _storeService.GetParentCategoriesListItems();
			return PartialView("_CreateCategory");
		}

		[HttpPost]
        public IActionResult CreateCategory(ProductCategory model)
        {
			if (ModelState.IsValid)
			{
				if(model.ParentId == 0)
				{
					model.ParentId = null;
				}

				bool res = _storeService.CreateCategoryByAdmin(model);
				if (res)
				{
                    return new JsonResult(new { status = true, mess = "ثبت دسته با موفقیت انجام شد" });
                }
				
			}
            return new JsonResult(new { status = false, mess = "خطا در ثبت دسته" });
        }


    }
}
