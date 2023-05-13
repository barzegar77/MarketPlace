using DataLayer.Context;
using DataLayer.Models.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ServiceLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISmsSender _smsSender;
		private readonly IStoreService _storeService;

		public IdentityService(ApplicationDbContext context,
            ISmsSender smsSender,
            IStoreService storeService)
        {
            _context = context;
            _smsSender = smsSender;
			_storeService = storeService;
		}



        #region admin Panel

        public BaseFilterViewModel<ListUserForAdminViewModel> GetAllUsersForAdmin(int pageIndex, string search)
        {
            var userList = _context.Users.Where(x => x.IsDeleted == false).OrderByDescending(x => x.RegisterTime).ToList();
            int take = 15;
            int howManyPageShow = 2;
            var pager = PagingHelper.Pager(pageIndex, userList.Count(), take, howManyPageShow);

            if(search != null)
            {
                userList = userList.Where(x => x.PhoneNumber.Contains(search) || x.DisplayName.Contains(search)).ToList();
            }

            var resault = userList.Select(x => new ListUserForAdminViewModel
            {
                ConfrimPhoneNumber=  x.ConfrimPhoneNumber,
                CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                DisplayName = x.DisplayName,
                PhoneNumber = x.PhoneNumber,
                UserId = x.UserId
            }).ToList();

            var outPut = PagingHelper.Pagination<ListUserForAdminViewModel>(resault, pager);

            BaseFilterViewModel<ListUserForAdminViewModel> res = new BaseFilterViewModel<ListUserForAdminViewModel>
            {
                EndPage = pager.EndPage,
                Entities = outPut,
                PageCount = pager.PageCount,
                StartPage = pager.StartPage,
                PageIndex = pageIndex
            };

            return res;
        }


        #endregion

        public bool UpdateUserInfo(ManagUserInfoViewModel model)
        {
            if (model != null)
            {
                var userInfo = _context.UserInfos.FirstOrDefault(x => x.Id == model.UserInfoId);
                userInfo.Adderss = model.Adderss;
				userInfo.IsShowAdderss = model.IsShowAdderss;
				userInfo.EmailAddress = model.EmailAddress;
                userInfo.IsShowEmailAddress = model.IsShowEmailAddress;
				userInfo.PhoneNumber = model.PhoneNumber;
				userInfo.IsShowPhoneNumber = model.IsShowPhoneNumber;
				userInfo.BankCartImage = model.BankCartImage;
                userInfo.BankCartNumber = model.BankCartNumber;
				userInfo.ShabaNumber = model.ShabaNumber;
				userInfo.CartMeliImage = model.CartMeliImage;
                userInfo.CodeMeli = model.CodeMeli;
                userInfo.Description = model.Description;
                userInfo.Logo = model.Logo;

                _context.UserInfos.Update(userInfo);
                _context.SaveChanges();
                return true;
                
            }
            return false;
        }


        public ManagUserInfoViewModel GetUserInfoForUpdate(int userInfoId)
        {
            return _context.UserInfos.Select(x => new ManagUserInfoViewModel
            {
                Adderss = x.Adderss,
                BankCartImage = x.BankCartImage,
                BankCartNumber = x.BankCartNumber,
                CartMeliImage = x.CartMeliImage,
                CodeMeli = x.CodeMeli,
                Description = x.Description,
                EmailAddress = x.EmailAddress,
                IsShowAdderss = x.IsShowAdderss,
                IsShowEmailAddress = x.IsShowEmailAddress,
                IsShowPhoneNumber = x.IsShowPhoneNumber,
                Logo = x.Logo,
                PhoneNumber = x.PhoneNumber,
                ShabaNumber = x.ShabaNumber,
                UserId = x.UserId,
                UserInfoId = x.Id
            }).FirstOrDefault(x => x.UserInfoId == userInfoId);
        }


		public int? IsHaveUserInfo(string phoneNumber)
        {
            int? userInfoId = _context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).UserInfoId;
            if(userInfoId != null || userInfoId > 0)
            {
                return userInfoId;
            }
            return 0;
        }

        public bool AddUserInfo(ManagUserInfoViewModel model)
        {
            if(model != null)
            {
                var userInfo = new UserInfo
                {
                    Adderss = model.Adderss,
                    BankCartImage = model.BankCartImage,
                    BankCartNumber = model.BankCartNumber,
                    CartMeliImage = model.CartMeliImage,
                    CodeMeli = model.CodeMeli,
                    Description = model.Description,
                    EmailAddress = model.EmailAddress,
                    IsShowAdderss = model.IsShowAdderss,
                    IsShowEmailAddress = model.IsShowEmailAddress,
                    IsShowPhoneNumber = model.IsShowPhoneNumber,
                    Logo = model.Logo,
                    PhoneNumber = model.PhoneNumber,
                    ShabaNumber = model.ShabaNumber,
                    UserId = model.UserId
                };

                _context.UserInfos.Add(userInfo);
                int res = _context.SaveChanges();

                if(res > 0)
                {
                    var user= _context.Users.Find(userInfo.UserId);
                    user.UserInfoId = userInfo.Id;
                    _context.Users.Update(user);
                    _context.SaveChanges();
					return true;
                }

            }
            return false;
        }

        public bool UpdateUserInfoFromUserPanel(UserInfoForUserPanelViewModel model)
        {
            if(model != null)
            {
                var user = _context.Users.Find(model.UserId);
                user.DisplayName = model.DispalyName;
                _context.Users.Update(user);
                int res = _context.SaveChanges();
                if (res > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public UserInfoForUserPanelViewModel GetUserInfoForUserPanelById(int id)
        {
            return _context.Users.Where(x => x.UserId == id)
                .Select(x => new UserInfoForUserPanelViewModel
                {
                    DispalyName = x.DisplayName,
                    PhoneNumber = x.PhoneNumber,
                    RegisterTime = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                    UserId = x.UserId
                }).FirstOrDefault();
        }

        public UserInfoForUserPanelViewModel GetUserInfoForUserPanel(string phoneNumber)
        {
            return _context.Users.Where(x => x.PhoneNumber == phoneNumber)
                .Select(x => new UserInfoForUserPanelViewModel
                {
                    DispalyName = x.DisplayName,
                    PhoneNumber = x.PhoneNumber,
                    RegisterTime = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                    UserId = x.UserId
                }).FirstOrDefault();
        }

		public UserPanelSideBarViewModel GetUserPanelSideBar(string phoneNumber)
        {
            var user = _context.Users.Include(x => x.Seller).Where(x => x.PhoneNumber == phoneNumber);
            int requestStatus = _storeService.GetSellerStatusByPhoneNumber(phoneNumber);
            if(requestStatus == 2)
			{
                return user.Select(x => new UserPanelSideBarViewModel
				  {
					  AvatarName = x.Avatar,
					  DisplayName = x.DisplayName,
					  IsSeller = true
				  }).FirstOrDefault();
			}
			return user.Select(x => new UserPanelSideBarViewModel
			{
				AvatarName = x.Avatar,
				DisplayName = x.DisplayName,
				IsSeller = false
			}).FirstOrDefault();
		}


        public string GetDisplayNameByPhoneNumber(string phone)
        {
            return _context.Users.FirstOrDefault(x => x.PhoneNumber == phone).DisplayName;
        }


        //-1 => ثبت نام یا شکست مواجه شد
        // 1=> ثبت نام با موفقیت انجام شد
        // -100 => کاربر قبلا ثبت نام کرده
        // -50 => بعد از 3 دقیقه باید تلاش کنید
        public int RegsiterByPhoneNumber(RegisterViewModel model)
        {
            int StatusPhoneNumber = IsExistPhoneNumber(model.PhoneNumber);

            if (StatusPhoneNumber == 1)
            {
                User user = new User
                {
                    ConfrimCode = GenerateVerifyCode(),
                    Avatar = "Default.png",
                    ConfrimCodeCreateDate = DateTime.Now,
                    ConfrimPhoneNumber = false,
                    DisplayName = model.DisplayName,
                    IsDeleted = false,
                    Password = PasswordHelper.EncodePasswordMd5(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    RegisterTime = DateTime.Now,
                };

                //todo => send sms
                _smsSender.SendSms(1, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

                _context.Users.Add(user);
                _context.SaveChanges();
                return 1;
            }
            if (StatusPhoneNumber == -100)
            {
                return -100;
            }
            if (StatusPhoneNumber == -50)
            {
                var user = _context.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);

                if (user.ConfrimCodeCreateDate.AddSeconds(20) > DateTime.Now)
                {
                    return -50;
                }

                user.DisplayName = model.DisplayName;
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
                user.RegisterTime = DateTime.Now;

                //todo => send sms

                _smsSender.SendSms(1, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

                _context.Users.Update(user);
                _context.SaveChanges();
                return 1;
            }

            return -1;

        }


        // 1 => شماره موبایل وجود ندارد
        // -100 => قبلا ثبت نام انجام شده
        // -50 => شماره موبایل تایید نشده
        // -1 => خطای دیتابیس
        public int IsExistPhoneNumber(string PhoneNumber)
        {
            var res = _context.Users.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

            if (res == null)
            {
                return 1;
            }
            if (res.ConfrimPhoneNumber == true)
            {
                return -100;
            }
            if (res.ConfrimPhoneNumber == false)
            {
                return -50;
            }

            return -1;
        }


        public string GenerateVerifyCode()
        {
            Random random = new Random();
            return random.Next(12345, 99999).ToString();
        }


        //-100 => کاربر وجود نداره
        // -50 => پسسورد اشتباهه
        // -150 => حساب کاربری فعال نیست 
        // -200 => کاربر حذف شده است
        public int GetUserStatusForLoginByPhoneNumber(string phoneNumber, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return -100;
            }
            if (user.Password != PasswordHelper.EncodePasswordMd5(password))
            {
                return -50;
            }
            if (user.ConfrimPhoneNumber == false)
            {
                return -150;
            }
            if (user.IsDeleted == true)
            {
                return -200;
            }

            return -1;
        }


        public int GetUserIdByPhoneNumber(string phoneNumber)
        {
            return _context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).UserId;
        }


        public bool IsPhoneNumberExist(string phoneNumber)
        {
            return _context.Users.Any(x => x.PhoneNumber == phoneNumber);
        }

        //-100 => کاربر یافت نشد
        //-50 => کد تایید منقضی شده است
        //-200 => کد تایید تطابق ندارد
        public int ConfrimPhoneNumber(string phoneNumber, string code)
        {
            var user = _context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return -100;
            }
            if (user.ConfrimCodeCreateDate.AddMinutes(15) < DateTime.Now)
            {
                return -50;
            }
            if (user.ConfrimCode != code)
            {
                return -200;
            }
            else
            {
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimPhoneNumber = true;

                _context.Users.Update(user);
                _context.SaveChanges();
                return 1;
            }

            return -1;
        }



        public int GetPhoneNumberStatusForForgotPass(string PhoneNumber)
        {


            var user = _context.Users.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

            if (user == null)
            {
                return -100;

            }
            if (user.IsDeleted == true)
            {
                return -50;
            }
            if (user.ConfrimPhoneNumber == false)
            {
                return -200;
            }

            else
            {
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;

                bool res = _smsSender.SendSms(2, user.PhoneNumber, user.DisplayName, user.ConfrimCode);


                //if(res == true)
                //{
                _context.Users.Update(user);
                _context.SaveChanges();
                return 1;
                //}
            }

            return -1;

        }


        public int ResetPasswordByMobile(RsetPasswordViewModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);

            if (user == null)
            {
                return -100;
            }
            if (user.ConfrimCodeCreateDate.AddMinutes(15) < DateTime.Now)
            {
                return -50;
            }
            if (user.ConfrimCode != model.Code)
            {
                return -200;
            }
            else
            {
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);

                _context.Users.Update(user);
                _context.SaveChanges();
                return 1;
            }

            return -1;
        }

        public bool CheckPerimission(int permissionId, string phoneNumber)
        {
            int userId = _context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).UserId;

            List<int> roleIds = _context.UserRoles.Where( x=> x.UserId == userId).Select(x=> x.RoleId).ToList();

            bool flag = false;

            if(!roleIds.Any())
            {
                flag = false;
            }
            else
            {
                foreach(int roleId in roleIds)
                {
                    foreach( var _rolePermission in _context.RolePermissions.Where(x => x.RoleId == roleId).ToList())
                    {
                        if(_rolePermission.PermissionId == permissionId)
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }



        public ManagUserInfoViewModel GetUserInfoByUserIdForAdmin(int userId)
        {
            return _context.UserInfos.Where(x => x.UserId == userId)
                .Select(x => new ManagUserInfoViewModel
                {
                    Adderss = x.Adderss,
                    BankCartImage = x.BankCartImage,
                    BankCartNumber = x.BankCartNumber,
                    CartMeliImage = x.CartMeliImage,
                    CodeMeli = x.CodeMeli,
                    Description = x.Description,
                    EmailAddress = x.EmailAddress,
                    IsShowAdderss = x.IsShowAdderss,
                    IsShowEmailAddress = x.IsShowEmailAddress,
                    IsShowPhoneNumber = x.IsShowPhoneNumber,
                    Logo = x.Logo,
                    PhoneNumber = x.PhoneNumber,
                    ShabaNumber = x.ShabaNumber,
                }).FirstOrDefault();
        }


        public bool IsUserHaveUserInfo(int userId)
        {
           return _context.UserInfos.Any(x => x.UserId == userId);
        }


	}
}
