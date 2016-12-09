namespace Movies.Models.Entities
{
    public class FilterParams
    {
        public string Genre { get; }

        public int Votes { get; }

        public double Imdb { get; }

        public int Tomatometer { get; }

        public int Metacritic { get; }

        public int From { get; }

        public FilterParams(string genre, int votes, double imdb, int tomatometer, int metacritic, int from)
        {
            Genre = genre;
            Votes = votes;
            Imdb = imdb;
            Tomatometer = tomatometer;
            Metacritic = metacritic;
            From = from;
        }
    }
}