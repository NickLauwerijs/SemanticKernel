using SemanticKernel.FakeAPI.Dto_s;
using SemanticKernel.FakeAPI.Interface;
using System.ComponentModel;

namespace SemanticKernel.FakeAPI
{
    public class MovieScheduleService : IMovieScheduleService
    {
        private List<WatchList> MovieWatchlists = new List<WatchList>();

        public void CreateWatchList(string watchList)
        {
            MovieWatchlists.Add(new WatchList(watchList));
        }

        public List<WatchList> GetAllWatchLists()
        {
            return MovieWatchlists;
        }

        public List<Movie> GetAllMoviesFromWatchList(string listName)
        {
            var watchList = MovieWatchlists.FirstOrDefault(x => x.Name == listName);
            if (watchList != null)
                return watchList.Movies;
            else
                throw new Exception("Watchlist does not exist");
        }

        public void SaveMovieToWatchList(string movieName, string listName)
        {
            var watchList = MovieWatchlists.FirstOrDefault(x => x.Name == listName);
            if (watchList != null)
                watchList.Movies.Add(new Movie(movieName));
            else
                throw new Exception("Watchlist does not exist");
        }
    }
}
