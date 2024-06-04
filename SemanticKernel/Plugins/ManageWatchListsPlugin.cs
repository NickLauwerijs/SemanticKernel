using Microsoft.SemanticKernel;
using SemanticKernel.FakeAPI.Dto_s;
using SemanticKernel.FakeAPI.Interface;
using System.ComponentModel;

namespace SemanticKernel.Plugins
{
    public class ManageWatchListsPlugin
    {
        private readonly IMovieScheduleService _movieScheduleService;
        public ManageWatchListsPlugin(IMovieScheduleService movieScheduleService)
        {
            _movieScheduleService = movieScheduleService;
        }

        [KernelFunction]
        [Description("Creates a watchlist")]
        public async Task CreateWatchList(
        Kernel kernel,
        [Description("the name of the watchlist the user wants create, it is obligatory that the user provides this")] string watchListName)
        {
            _movieScheduleService.CreateWatchList(watchListName);
        }

        [KernelFunction]
        [Description("gets all the watchlist for this user")]
        public async Task<List<WatchList>> GetAllWatchLists(
        Kernel kernel)
        {
            return _movieScheduleService.GetAllWatchLists();
        }

        [KernelFunction]
        [Description("Add movie to watchlist")]
        public async Task AddMovieToWatchList(
        Kernel kernel,
        [Description("the name of the watchlist that the movie need to be added to, it is obligatory that the user provides this")] string watchListName,
        [Description("the name of the movie that needs to be added to the watchlist, it is obligatory that the user provides this")] string movieName)
        {
            _movieScheduleService.SaveMovieToWatchList(movieName,watchListName);
        }
    }
}
