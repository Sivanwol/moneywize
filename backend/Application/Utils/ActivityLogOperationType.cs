namespace Application.Utils;

public static class ActivityLogOperationType {

    #region Auth

    public const string UserLogin = "UserLogin";
    public const string UserLogout = "UserLogout";
    public const string UserRegister = "UserRegister";
    public const string UserUpdate = "UserUpdate";
    public const string UserDelete = "UserDelete";
    public const string UserExport = "UserExport";
    public const string UserChangePassword = "UserChangePassword";
    public const string UserResetPassword = "UserResetPassword";
    public const string UserForgotPassword = "UserForgotPassword";
    public const string UserSendOTP = "UserSendOTP";
    public const string UserVerifyOTP = "UserVerifyOTP";

    #endregion

    #region User Operations

    public const string UserCreate = "UserCreate";
    public const string UserForceUpdate = "UserForceUpdate";
    public const string ListOfUsers = "ListOfUsers";

    #endregion

}