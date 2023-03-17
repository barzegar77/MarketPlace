using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ServiceLayer.Services.Interfaces
{
    public interface IIdentityService
    {
        #region adminpanel
        BaseFilterViewModel<ListUserForAdminViewModel> GetAllUsersForAdmin(int pageIndex, string search);
        #endregion

        bool UpdateUserInfo(ManagUserInfoViewModel model);

		ManagUserInfoViewModel GetUserInfoForUpdate(int userInfoId);

		int? IsHaveUserInfo(string phoneNumber);
		bool AddUserInfo(ManagUserInfoViewModel model);
        bool UpdateUserInfoFromUserPanel(UserInfoForUserPanelViewModel model);
        UserInfoForUserPanelViewModel GetUserInfoForUserPanelById(int id);
        UserInfoForUserPanelViewModel GetUserInfoForUserPanel(string phoneNumber);
		UserPanelSideBarViewModel GetUserPanelSideBar(string phoneNumber);
        string GetDisplayNameByPhoneNumber(string phone);

        int RegsiterByPhoneNumber(RegisterViewModel model);
        int IsExistPhoneNumber(string PhoneNumber);
        int GetUserStatusForLoginByPhoneNumber(string phoneNumber, string password);
        int GetUserIdByPhoneNumber(string phoneNumber);
        bool IsPhoneNumberExist(string phoneNumber);
        int ConfrimPhoneNumber(string phoneNumber, string code);
        int GetPhoneNumberStatusForForgotPass(string PhoneNumber);
        int ResetPasswordByMobile(RsetPasswordViewModel model);

        bool CheckPerimission(int permissionId, string phoneNumber);


        ManagUserInfoViewModel GetUserInfoByUserIdForAdmin(int userId);
        bool IsUserHaveUserInfo(int userId);




	}
}
