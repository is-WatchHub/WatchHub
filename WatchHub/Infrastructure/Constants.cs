namespace Infrastructure
{
    internal static class Constants
    {
        #region UserEntity

        public const string USER_TABLE_NAME = "user";
        public const string USER_ID_COLUMN_NAME = "user_id";
        public const string USER_LOGIN_COLUMN_NAME = "login";
        public const string USER_PASSWORD_COLUMN_NAME = "password";
        public const string USER_EMAIL_COLUMN_NAME = "email";
        public const string USER_ROLE_COLUMN_NAME = "role";

        #endregion

        #region MovieEntity

        public const string MOVIE_TABLE_NAME = "movie";
        public const string MOVIE_ID_COLUMN_NAME = "movie_id";
        public const string MOVIE_TITLE_COLUMN_NAME = "title";
        public const string MOVIE_DESCRIPTION_COLUMN_NAME = "description";
        public const string MOVIE_GENRE_COLUMN_NAME = "genre";
        public const string MOVIE_CONTENT_URL_COLUMN_NAME = "content_url";
        public const string MOVIE_COVER_IMAGE_URL_COLUMN_NAME = "image_url";

        #endregion
    }
}