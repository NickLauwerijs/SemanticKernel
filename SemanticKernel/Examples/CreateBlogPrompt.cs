using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Planning.Handlebars;
using SemanticKernel.Models;
using SemanticKernel.Plugins.WriteBlog;
using System.Text;

namespace SemanticKernel.Examples
{
    public class CreateBlogPrompt
    {
        private readonly Kernel _kernel;

        public CreateBlogPrompt(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Execute()
        {

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++ Write blog with simple prompt.
            //Console.Write("Please enter the topic: ");
            //string? topic = Console.ReadLine();
            //string prompt = $"""
            //Write a blog with the following topic:
            //{topic}
            //""";

            //var promptConfig = new PromptTemplateConfig
            //{
            //    Template = $@"Write a blog with the following topic:
            //                {topic}",
            //    ExecutionSettings =
            //{
            //        {   "default",
            //            new OpenAIPromptExecutionSettings()
            //            {
            //                MaxTokens = 4000, Temperature = 0.2, TopP = 0.5,
            //            }
            //        }

            //}
            //};

            //var writeBlog = _kernel.CreateFunctionFromPrompt(promptConfig);

            // var blog = await _kernel.InvokeAsync(writeBlog);

            //Console.WriteLine(blog);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Generate Title for blog by invoking plugin function

            //Console.Write("Please enter the topic: ");
            //string? topic = Console.ReadLine();


            //var plugin = _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteBlog");

            //var title = await _kernel.InvokeAsync(plugin["Title"], new()
            //{
            //    {"topic", topic }
            //});

            //Console.WriteLine(title);

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Generate Subtitle for blog based on title (call function inside function template) 

            //Console.Write("Please enter the topic: ");
            //string? topic = Console.ReadLine();


            //var plugin = _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteBlog");

            //var title = await _kernel.InvokeAsync(plugin["SubTitle"], new()
            //{
            //    {"topic", topic }
            //});

            //Console.WriteLine(title);

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Generate Cover using title and subtitle with DALL-E model
            //            Console.Write("Please enter the topic: ");
            //            string? topic = Console.ReadLine();


            //            var writePlugin = _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteBlog");
            //            var kernelArguments = new KernelArguments()
            //            {
            //                {"topic", topic}
            //            };

            //            var title = await _kernel.InvokeAsync(writePlugin["title"], kernelArguments);
            //            kernelArguments.Add("title", title);
            //            Console.WriteLine(title);

            //            var subtitle = await _kernel.InvokeAsync(writePlugin["subtitle"], kernelArguments);
            //            kernelArguments.Add("subtitle", subtitle);
            //            Console.WriteLine(subtitle);

            //            var cover = await _kernel.InvokeAsync(writePlugin["Cover"], kernelArguments);
            //            Console.WriteLine(cover);

            //#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            //            ITextToImageService dallE = _kernel.GetRequiredService<ITextToImageService>();
            //            var image = await dallE.GenerateImageAsync(cover.ToString(), 1024, 1024) ;
            //#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            //            using (WebClient client = new WebClient())
            //            {
            //                string url = image;  
            //                Uri uri = new Uri(url);
            //                string localPath = System.IO.Path.GetFileName(uri.LocalPath);

            //                client.DownloadFile(url, localPath);
            //}

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Capture result of each function and pass data to next function using kernelArguments

            //            Console.Write("Please enter the topic: ");
            //            string? topic = Console.ReadLine();


            //            var writePlugin = _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteBlog");
            //            var captureBlog = new CaptureBlog();
            //            var capturePlugin = _kernel.ImportPluginFromObject(captureBlog);

            //            var kernelArguments = new KernelArguments()
            //                        {
            //                            {"topic", topic}
            //                        };

            //            await _kernel.InvokeAsync(capturePlugin["topic"], kernelArguments);

            //            kernelArguments.Add("title", await _kernel.InvokeAsync(writePlugin["title"], kernelArguments));
            //            await _kernel.InvokeAsync(capturePlugin["title"], kernelArguments);
            //            Console.WriteLine(captureBlog.Blog.Title);

            //            kernelArguments.Add("subtitle", await _kernel.InvokeAsync(writePlugin["subtitle"], kernelArguments));
            //            await _kernel.InvokeAsync(capturePlugin["subtitle"], kernelArguments);
            //            Console.WriteLine(captureBlog.Blog.Subtitle);

            //            kernelArguments.Add("cover", await _kernel.InvokeAsync(writePlugin["cover"], kernelArguments));
            //            await _kernel.InvokeAsync(capturePlugin["cover"], kernelArguments);
            //            Console.WriteLine(captureBlog.Blog.CoverDescription);


            //            kernelArguments.Add("tableOfContents", await _kernel.InvokeAsync(writePlugin["tableOfContents"], kernelArguments));
            //            await _kernel.InvokeAsync(capturePlugin["tableOfContents"], kernelArguments);
            //            Console.WriteLine(captureBlog.Blog.TableOfContents);

            //            kernelArguments.Add("chapters", await _kernel.InvokeAsync(writePlugin["chapters"], kernelArguments));
            //            await _kernel.InvokeAsync(capturePlugin["chapters"], kernelArguments);
            //            Console.WriteLine(captureBlog.Blog.Chapters);




            //#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            //            ITextToImageService dallE = _kernel.GetRequiredService<ITextToImageService>();
            //            captureBlog.Blog.CoverUrl = await dallE.GenerateImageAsync(captureBlog.Blog.CoverDescription.ToString(), 1024, 1024);
            //#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            //            //using (WebClient client = new WebClient())
            //            //{
            //            //    string url = image;
            //            //    Uri uri = new Uri(url);
            //            //    string localPath = System.IO.Path.GetFileName(uri.LocalPath);

            //            //    client.DownloadFile(url, localPath);
            //            //}

            //            SaveBlogToFile(captureBlog.Blog, "blog.md");

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Console.Write("What's up ! what do you want me to do ?");
            string? request = Console.ReadLine();


            var writePlugin = _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteBlog");
            var captureBlog = new CaptureBlog();
            var capturePlugin = _kernel.ImportPluginFromObject(captureBlog);

            //var kernelArguments = new KernelArguments()
            //                        {
            //                            {"topic", topic}
            //                        };

            //await _kernel.InvokeAsync(capturePlugin["topic"], kernelArguments);

#pragma warning disable SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true});
            var plan = await planner.CreatePlanAsync(_kernel, request);
            Console.WriteLine(plan.ToString());
            var result = await plan.InvokeAsync(_kernel);
#pragma warning restore SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            Console.WriteLine("Result:");
            Console.WriteLine(result);
        }


        public void SaveBlogToFile(Blog blog, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"# {blog.Title}");
            sb.AppendLine($"{blog.Subtitle}\r\n");
            sb.AppendLine($"![{blog.Title}]({blog.CoverUrl})\r\n");
            sb.AppendLine($"{blog.TableOfContents}\r\n");
            sb.AppendLine($"{blog.Chapters}\r\n");
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(sb.ToString());
            }
        }
    }
}
