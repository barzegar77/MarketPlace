using DataLayer.Models.Store;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.StoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
	public interface IStoreService
	{
		bool SendRequestForSalePanel(ManageSellerViewModel model);
		BaseFilterViewModel<ListSellerViewModel> GetAllSellersForAdmin(int pageIndex, string search);
		ManageSellerViewModel GetSellerForUpdate(int sellerId);
		bool UpdateSellerRequest(ManageSellerViewModel model);
		int GetSellerStatusByPhoneNumber(string phoneNumber);

		BaseFilterViewModel<ListProductForSellerViewModel> GetProductListForSeller(int pageIndex, string search, int userId);
		List<ProductCategory> GetCategoriesForAdmin();
		bool CreateCategoryByAdmin(ProductCategory category);
		List<SelectListItem> GetParentCategoriesListItems();

		bool CreateProductBySeller(ManageProductBySellerViewModel model, int sellerId);
		int? GetSelleridByUserName(string userName);
        List<SelectListItem> GetSubCategoriesListItems();

    }
}
