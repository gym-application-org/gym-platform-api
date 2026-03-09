namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string SectionName = "Auth";

    public const string EmailAuthenticatorDontExists = "EmailAuthenticatorDontExists";
    public const string OtpAuthenticatorDontExists = "OtpAuthenticatorDontExists";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "AlreadyVerifiedOtpAuthenticatorIsExists";
    public const string EmailActivationKeyDontExists = "EmailActivationKeyDontExists";
    public const string UserDontExists = "UserDontExists";
    public const string UserHaveAlreadyAAuthenticator = "UserHaveAlreadyAAuthenticator";
    public const string RefreshDontExists = "RefreshDontExists";
    public const string InvalidRefreshToken = "InvalidRefreshToken";
    public const string UserMailAlreadyExists = "UserMailAlreadyExists";
    public const string PasswordDontMatch = "PasswordDontMatch";
    public const string UserActionTokenNotExists = "UserActionTokenNotExists";
    public const string UserActionTokenRevoked = "UserActionTokenRevoked";
    public const string UserActionTokenUsed = "UserActionTokenUsed";
    public const string UserActionTokenExpired = "UserActionTokenExpired";
    public const string MemberShouldBeExists = "MemberShouldBeExists";
    public const string StaffShouldBeExists = "StaffShouldBeExists";
}
