namespace Infrastructure
{
    internal static class UserTableConstants
    {
        public const string USER_TABLE_NAME = "user";

        public const string USER_ID_COLUMN_NAME = "user_id";

        public const string USER_LOGIN_COLUMN_NAME = "login";
        public const int USER_LOGIN_COLUMN_MAX_LENGTH = 20;

        public const string USER_PASSWORD_COLUMN_NAME = "password";
        public const int USER_PASSWORD_COLUMN_MAX_LENGTH = 150;

        public const string USER_EMAIL_COLUMN_NAME = "email";
        public const int USER_EMAIL_COLUMN_MAX_LENGTH = 150;

        public const string USER_ROLE_COLUMN_NAME = "role";
    }
}
