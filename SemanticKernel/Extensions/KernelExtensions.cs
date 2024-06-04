using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel.Extensions
{
    public static class KernelExtensions
    {
        public static async Task<KernelFunction> LoadFunctionFromYaml(this Kernel kernel, string resourceName)
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)!);
            return kernel.CreateFunctionFromPromptYaml(
                await reader.ReadToEndAsync(),
                promptTemplateFactory: new HandlebarsPromptTemplateFactory()
            );
        }
    }
}
