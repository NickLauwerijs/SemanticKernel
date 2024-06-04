using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel.Planners
{
    /// <summary>
    /// 
    /// </summary>
    public class MoviePlanner
    {

        [KernelFunction]
        [Description("Returns back the required steps necessary to schedule a movie.")]
        [return: Description("The list of steps needed to schedule a movie in my application")]
        public async Task<string> GenerateRequiredStepsAsync(
            Kernel kernel,
            [Description("Name of the movie you would like to schedule")] string movieName,
            [Description("Date when you want to schedule the movie")] string schedulingDate
        )
        {
            // Prompt the LLM to generate a list of steps to complete the task
            //    var result = await kernel.InvokePromptAsync($"""
            //I'm going to write an email to {recipients} about {topic} on behalf of a user.
            //Before I do that, can you succinctly recommend the top 3 steps I should take in a numbered list?
            //I want to make sure I don't forget anything that would help my user's email sound more professional.
            //""", new() {
            //    { "topic", topic },
            //    { "recipients", recipients }
            //});

            var result = await kernel.InvokePromptAsync($"""
            I'm going to schedule the following movie: {movieName} on date: {schedulingDate}.
            Could you recommend me the 2 steps that are neccesary to do this ? 
            """, new() {
                { "movieName", movieName },
                { "schedulingDate", schedulingDate }
            });

            // Return the plan back to the agent
            return result.ToString();
        }
    }
}
