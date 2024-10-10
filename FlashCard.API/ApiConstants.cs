namespace FlashCard.API
{
    public static class ApiConstants
    {
        public const string HeaderReferenceIDKey = "Request-Id";

        public const string RegistrationIdKey = "registrationId";
        public const int DefaultPageNo = 1;
        public const int DefaultPageSize = 5;
        public const string AdminAlias = "Admin";
        public const string RegistrationHash = "registrationHash";
        public const string DateFormat = "yyyy-MM-dd";
        public const string InvalidRegistrationId = "Invalid registration Id";
        public const string ExceededOTPRequestLimit = "You exceeded OTP request max attempt";
        public const string InvalidMobilePhone = "Unable to send OTP to this phone number";
        public const string InvalidUserInfo = "The registration details you have entered do not match our system.";
        public const string InvalidOTP = "Invalid OTP";
    }
}
