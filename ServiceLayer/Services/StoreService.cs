using DataLayer.Context;
using DataLayer.Models.Store;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.IdentityViewModels;
using ServiceLayer.ViewModels.StoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class StoreService :IStoreService
	{
		private readonly ApplicationDbContext _context;

		public StoreService(ApplicationDbContext context)
        {
			_context = context;
		}


		public bool SendRequestForSalePanel(ManageSellerViewModel model)
		{
			if(model != null)
			{
				var seller = new Seller 
				{ 
				CreateDate = DateTime.Now,
				Description = model.Description,
				IsDeleted = false,
				Request = 1,
				UserId = model.UserId
				};

				_context.Sellers.Add(seller);
				_context.SaveChanges();
				return true;

			}
			return false;
		}

		public BaseFilterViewModel<ListSellerViewModel> GetAllSellersForAdmin(int pageIndex, string search)
		{
			var sellerList = _context.Sellers.Include(x => x.User).Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreateDate).ToList();
			int take = 15;
			int howManyPageShow = 2;
			var pager = PagingHelper.Pager(pageIndex, sellerList.Count(), take, howManyPageShow);

			if(search != null)
			{
				sellerList = sellerList.Where(x => x.User.PhoneNumber.Contains(search) || x.User.DisplayName.Contains(search)).ToList();
			}

			var resault = sellerList.Select(x => new ListSellerViewModel
			{
				CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.CreateDate, false),
				DisplayName = x.User.DisplayName,
				PhoneNumber = x.User.PhoneNumber,
				Request = x.Request,
				Id = x.Id
			}).ToList();

			var outPut = PagingHelper.Pagination<ListSellerViewModel>(resault, pager);

			BaseFilterViewModel<ListSellerViewModel> res = new BaseFilterViewModel<ListSellerViewModel>
			{
				EndPage = pager.EndPage,
				Entities = outPut,
				PageCount = pager.PageCount,
				StartPage = pager.StartPage,
				PageIndex = pageIndex
			};

			return res;
		}


		
	}
}
