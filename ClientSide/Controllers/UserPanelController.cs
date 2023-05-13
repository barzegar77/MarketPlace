using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;
using ServiceLayer.ViewModels.StoreViewModels;

namespace ClientSide.Controllers
{
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class UserPanelController : Controller
	{
		private readonly IIdentityService _identityService;
		private readonly IFileUploader _fileUploader;
		private readonly IStoreService _storeService;

		public UserPanelController(IIdentityService identityService,
			IFileUploader fileUploader,
			IStoreService storeService)
        {
			_identityService = identityService;
			_fileUploader = fileUploader;
			_storeService = storeService;
		}
        public IActionResult Dashboard()
		{
			var userInfo = _identityService.GetUserInfoForUserPanel(User.Identity.Name);
			return View(userInfo);
		}

		public IActionResult LoadUserInfo(int id)
		{
			var model = _identityService.GetUserInfoForUserPanelById(id);

			return PartialView("_loadUserInfo", model);
		}

		[HttpPost]
		public IActionResult UpdateUserInfo(UserInfoForUserPanelViewModel model)
		{ 
			if (ModelState.IsValid)
			{
				bool res = _identityService.UpdateUserInfoFromUserPanel(model);
				if (res)
				{
                    return Json(new { status = "success", message = "ویرایش کاربر با موفقیت انجام شد" });
                }
			}
			return Json(new { status = "error", message = "خطا در ویرایش کاربر" });
		}


		public IActionResult UserInfo()
		{
			int? res = _identityService.IsHaveUserInfo(User.Identity.Name);
			if(res > 0)
			{
				var model = _identityService.GetUserInfoForUpdate((int)res);
				return View(model);
			}
			return View();
		}

		[HttpPost]
		public IActionResult UserInfo(ManagUserInfoViewModel model)
		{
			int userId =	_identityService.GetUserIdByPhoneNumber(User.Identity.Name);
			model.UserId = userId;

			if (ModelState.IsValid)
			{
				if(model.UserInfoId > 0)
				{
					bool res2 = _identityService.UpdateUserInfo(model);
					if (res2 == true)
					{
						TempData["success"] = "اطلاعات کاربری با موفقیت ویرایش شد";
						return RedirectToAction("Dashboard");
					}
				}
			 bool res =	_identityService.AddUserInfo(model);
				if(res == true)
				{
					TempData["success"] = "عملیات با موفقیت انجام شد";
					return RedirectToAction("Dashboard");
				}
			}
			TempData["error"] = "خطا در برقراری ارتباط با سرور";
			return View(model);
		}



		public IActionResult RequestForSalePanel()
		{
			ViewBag.StatusSeller = _storeService.GetSellerStatusByPhoneNumber(User.Identity.Name);
			return View();
		}
		[HttpPost]
		public IActionResult RequestForSalePanel(ManageSellerViewModel model)
		{
			int userId = _identityService.GetUserIdByPhoneNumber(User.Identity.Name);
			model.UserId = userId;

			if (ModelState.IsValid)
			{
				bool res = _storeService.SendRequestForSalePanel(model);
				if (res)
				{
					TempData["success"] = "عملیات با موفقیت انجام شد";
					return RedirectToAction("Dashboard");
				}
			}
			TempData["error"] = "خطا در برقراری ارتباط با سرور";
			ViewBag.StatusSeller = _storeService.GetSellerStatusByPhoneNumber(User.Identity.Name);
			return View();
		}


		public IActionResult ProductList(int pageId = 1, string search = "")
		{
			int userId = _identityService.GetUserIdByPhoneNumber(User.Identity.Name);
			var productList = _storeService.GetProductListForSeller(pageId, search, userId);
			return View(productList);
		}


		public IActionResult CreateProduct()
		{
			ViewBag.listCategoris = _storeService.GetSubCategoriesListItems();
			return View();
		}

		[HttpPost]
		public IActionResult CreateProduct(ManageProductBySellerViewModel model)
		{
			return View();
		}


        #region dropzone

        public IActionResult UploadBankCart(List<IFormFile> files)
		{
			if (files.Count() > 0)
			{
				string path = "\\appdata\\user\\bankCards\\";
				List<string> fileNames = _fileUploader.UploadFile(files, path);
				return Json(new { status = "success", data = fileNames.FirstOrDefault() });
			}

			return Json(new { status = "error", message = "خطا" });
		}


		public IActionResult UploadCartMeli(List<IFormFile> files)
		{
			if (files.Count() > 0)
			{
				string path = "\\appdata\\user\\cartMelis\\";
				List<string> fileNames = _fileUploader.UploadFile(files, path);
				return Json(new { status = "success", data = fileNames.FirstOrDefault() });
			}


			return Json(new { status = "error", message = "خطا" });
		}

		public IActionResult UploadUserLogo(List<IFormFile> files)
		{
			if (files.Count() > 0)
			{
				string path = "\\appdata\\user\\logo\\";
				List<string> fileNames = _fileUploader.UploadFile(files, path);
				return Json(new { status = "success", data = fileNames.FirstOrDefault() });
			}
			return Json(new { status = "error", message = "خطا" });
		}


        public IActionResult UploadProductIndexImage(List<IFormFile> files)
        {
            if (files.Count() > 0)
            {
                string path = "\\appdata\\product\\img\\";
                List<string> fileNames = _fileUploader.UploadFile(files, path);
                return Json(new { status = "success", data = fileNames.FirstOrDefault() });
            }
            return Json(new { status = "error", message = "خطا" });
        }

        #endregion


    }
}
