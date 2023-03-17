using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.StoreViewModels
{
	public class ListSellerViewModel
	{
		public int Id { get; set; }
		public int Request { get; set; }
		public string CreateDate { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
