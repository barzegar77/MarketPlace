using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Store
{
    public class ProductPrice
    {
        [Key]
        public int Id { get; set; }

        public string Lable { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public int DisountPercentage { get; set; }

        public int Weight { get; set; }

        public int PercentageSite { get; set; }

        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public int ConfrimStatus { get; set; }

        #region rel

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        #endregion
    }
}
