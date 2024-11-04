namespace Infrastructure
{
    internal static class MovieTableConstants
    {
        public const string MOVIE_TABLE_NAME = "movie";

        public const string MOVIE_ID_COLUMN_NAME = "movie_id";

        public const string MOVIE_TITLE_COLUMN_NAME = "title";
        public const int MOVIE_TITLE_COLUMN_MAX_LENGTH = 100;

        public const string MOVIE_DESCRIPTION_COLUMN_NAME = "description";
        public const int MOVIE_DESCRIPTION_COLUMN_MAX_LENGTH = 500;

        public const string MOVIE_GENRE_COLUMN_NAME = "genre";

        public const string MOVIE_CONTENT_URL_COLUMN_NAME = "content_url";
        public const int MOVIE_CONTENT_URL_COLUMN_MAX_LENGTH = 150;

        public const string MOVIE_COVER_IMAGE_URL_COLUMN_NAME = "image_url";
        public const int MOVIE_COVER_IMAGE_URL_COLUMN_MAX_LENGTH = 150;
    }
}
