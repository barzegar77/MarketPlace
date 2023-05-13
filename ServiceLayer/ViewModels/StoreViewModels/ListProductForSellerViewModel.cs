using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.StoreViewModels
{
    public class ListProductForSellerViewModel
    {
        public int Id { get; set; }
        public string CreateDate { get; set; }
        public string EnTitle { get; set; }
        public string FaTitle { get; set; }
        public string DefaultPrice { get; set; }
        public string DafaultImage { get; set; }
    }
}
