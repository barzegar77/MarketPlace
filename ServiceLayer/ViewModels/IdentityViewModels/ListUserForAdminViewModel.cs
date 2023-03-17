using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class ListUserForAdminViewModel
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public bool ConfrimPhoneNumber { get; set; }
        public string CreateDate { get; set; }
    }
}
