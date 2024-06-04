using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel.Plugins.WriteArtcle
{
    public class ImageGenerator
    {
        [KernelFunction]
        [Description("Generates an image")]
        public async Task<string> GenerateImage(
       Kernel kernel,
       [Description("The description of how the image should look like")] string imageDescription)
        {
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            ITextToImageService dallE = kernel.GetRequiredService<ITextToImageService>();
            return await dallE.GenerateImageAsync(imageDescription, 1024, 1024);
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        }
    }
}
