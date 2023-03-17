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
	}
}
