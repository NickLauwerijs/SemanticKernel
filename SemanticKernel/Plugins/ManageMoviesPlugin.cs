using Microsoft.SemanticKernel;
using SemanticKernel.FakeAPI.Interface;
using System.ComponentModel;

namespace SemanticKernel.Plugins
{
    public class ManageMoviesPlugin
    {
        private readonly IMovieScheduleService _movieScheduleService;
        public ManageMoviesPlugin(IMovieScheduleService movieScheduleService)
        {
            _movieScheduleService = movieScheduleService;
        }

        [KernelFunction]
        [Description("Schedules a movie")]
        public async Task ScheduleMovieAsync(
        Kernel kernel,
        [Description("the name of the movie the user wants to schedule")] string movieName,
        [Description("the date when the user wants to schedule provided in following format dd/mm/yyyy")] DateTime scheduleDate)
        {
            Console.WriteLine($"MovieScheduled : {movieName} on : {scheduleDate}");
        }

       // [KernelFunction]
       // [Description("Returns information about a movie that the user is intrested in")]
       // public async Task ShowMovieDetailsAsync(
       //Kernel kernel,
       //[Description("the name of the movie the user wants more information")] string movieName)
       // {
       //     var result = await kernel.InvokePromptAsync($"Provide details about {movieName} , provide details in following format : TITLE and then bulletpoints for actors, plot, director");
       //     Console.WriteLine(result.ToString());
       // }
    }
}
