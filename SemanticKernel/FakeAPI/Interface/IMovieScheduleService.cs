
using SemanticKernel.FakeAPI.Dto_s;

namespace SemanticKernel.FakeAPI.Interface
{
    public interface IMovieScheduleService
    {
        public void SaveMovieToWatchList(string movieName, string listName);
        public List<Movie> GetAllMoviesFromWatchList(string listName);

        public void CreateWatchList(string watchList);
        public List<WatchList> GetAllWatchLists();



    }
}
