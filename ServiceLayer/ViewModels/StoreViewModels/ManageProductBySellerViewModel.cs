using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.StoreViewModels
{
    public class ManageProductBySellerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان فارسی محصول")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی محصول")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string EnTitle { get; set; }

        [Display(Name = "ویژگی های محصول")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string ProductFeatures { get; set; }

        [Display(Name = "تصویر پیش فرض")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string IndexImage1 { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Tags { get; set; }

        [Display(Name = "نوضیحات مختصر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string ShortDescripton { get; set; }

        [Display(Name = "توضیحات کامل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Descripton { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int CategoryId { get; set; }

        #region price

        [Display(Name = "عنوان قیمت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Lable { get; set; }


        [Display(Name = "قیمت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int Price { get; set; }

        [Display(Name = "تعداد محصول")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int Count { get; set; }

        [Display(Name = "وزن")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int Weight { get; set; }

        #endregion

    }
}
