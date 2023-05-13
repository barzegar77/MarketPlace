using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Store
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string ProductFeatures { get; set; }

        public string IndexImage1 { get; set; }
        public string IndexImage2 { get; set; }

        public string Tags { get; set; }

        public string ShortDescripton { get; set; }
        public string Descripton { get; set; }


        public int ConfrimStatus { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }




        #region rel 

        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public Seller Seller { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public ProductCategory ProductCategory { get; set; }

        public IEnumerable<ProductPrice> ProductPrices { get; set; }

        #endregion
    }
}
