namespace Movies.Clients.Helpers
{
    public abstract class TmdbClientHelper
    {
        public static string GenresEndPoint = "genre/movie/list";

        public static string MoviesByGenreEndPoint = "genre/{genreId}/movies";

        public static string MovieByIdEndPoint = "movie/{movieId}";

        public static string MovieByImdbIdEndPoint = "find/{imdbId}";

        public static string ApiKeyFlag = "api_key=";

        public static string PageFlag = "page=";

        public static string ExternalIdsFlag = "append_to_response=external_ids";

        public static string ExternalSourceFlag = "external_source=imdb_id";
    }
}