using DataLayer.Models.Store;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Identity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string PhoneNumber { get; set; }

        public bool ConfrimPhoneNumber { get; set; }
        public string ConfrimCode { get; set; }
        public DateTime ConfrimCodeCreateDate { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public DateTime RegisterTime { get; set; }

        public bool IsDeleted { get; set; }

        public string Avatar { get; set; }


        #region rel

        public IEnumerable<UserRole> UserRoles { get; set; }

        public int? UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public UserInfo? UserInfo { get; set; }


		public int? SellerId { get; set; }
		[ForeignKey("SellerId")]
		public Seller? Seller { get; set; }
		#endregion

	}
}
