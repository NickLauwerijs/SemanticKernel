using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel.Examples
{
    public class SimplePrompt
    {
        private readonly Kernel _kernel;

        public SimplePrompt(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Execute()
        {
            Console.Write("Your request: ");
            string request = Console.ReadLine()!;

            //string prompt = string.Format(Prompts.AskIntent, request);
            //string prompt = string.Format(Prompts.AskIntent_ProvideOptions, request);
            //string prompt = string.Format(Prompts.AskIntent_ProvideOptions_Structure, request);
            //string prompt = string.Format(Prompts.AskIntent_ProvideOptions_Structure_Json, request);
            //string prompt = string.Format(Prompts.AskIntent_ProvideOptions_Structure_FewShot, request);
            string prompt = string.Format(Prompts.Prompts.AskIntent_ProvideOptions_Structure_Unknown, request);
            //string prompt = string.Format(Prompts.AskIntent_ProvideOptions_Structure_Context_Roles, request);

            var result = await _kernel.InvokePromptAsync(prompt);

            Console.WriteLine(result);
        }
    }
}
