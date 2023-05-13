using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Store
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string IconName { get; set; }
        public bool IsDeleted { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ProductCategory? Parent { get; set; }
    }
}
