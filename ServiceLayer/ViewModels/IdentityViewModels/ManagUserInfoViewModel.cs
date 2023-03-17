using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class ManagUserInfoViewModel
    {

        public int UserInfoId { get; set; }

        [Display(Name = "شماره همراه نمایشی")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "آیا شماره تلفن در سایت نمایش داده شود؟")]
        public bool IsShowPhoneNumber { get; set; }

        [Display(Name = "ایمیل نمایشی")]
        public string? EmailAddress { get; set; }
        [Display(Name = "آیا ایمیل در سایت نمایش داده شود؟")]
        public bool IsShowEmailAddress { get; set; }
        [Display(Name = "آدرس")]
        public string? Adderss { get; set; }
        [Display(Name = "آیا آدرس در سایت نمایش داده شود؟")]
        public bool IsShowAdderss { get; set; }
        [Display(Name = "بیوگرافی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Description { get; set; }
		[Display(Name = "لوگوی شخصی")]
		public string? Logo { get; set; }
        [Display(Name = "کد ملی")]
        public string? CodeMeli { get; set; }
		[Display(Name = "تصویر کارت ملی")]
		public string? CartMeliImage { get; set; }
        [Display(Name = "شماره شبا")]
        public string? ShabaNumber { get; set; }
        [Display(Name = "شماره کارت بانکی")]
        public string? BankCartNumber { get; set; }
		[Display(Name = "تصویر کارت بانکی")]
		public string? BankCartImage { get; set; }
        public int UserId { get; set; }
    }
}
