using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Identity
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsShowPhoneNumber { get; set; }

        public string? EmailAddress { get; set;}
        public bool IsShowEmailAddress { get; set; }

        public string? Adderss { get; set; }
        public bool IsShowAdderss { get; set; }

        public string Description { get; set; }

        public string? Logo { get; set; }
        public string? CodeMeli { get; set; }
        public string? CartMeliImage { get; set; }
        public string? ShabaNumber { get; set; }
        public string? BankCartNumber { get; set; }
        public string? BankCartImage { get; set; }


        #region rel

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        #endregion
    }
}
