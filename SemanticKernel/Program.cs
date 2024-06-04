using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.Plugins.WriteArtcle;

public class Program
{
    private static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
                    .AddJsonFile("config/appsettings.json")
                    .Build();

        var builder = Kernel.CreateBuilder();

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        builder.AddOpenAIChatCompletion(
            config["DEPLOYMENT_MODEL"],
            config["API_KEY"])
            .AddOpenAITextToImage(config["API_KEY"]);
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        Kernel kernel = builder.Build();






        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ EXPLICIT FUNCTION CALLING
        //var createArticle = new CreateArtcle(kernel);
        //await createArticle.Execute();

        //Console.WriteLine("Please enter the topic:");
        //string? topic = Console.ReadLine();
        //var plugin = kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteArticle");

        //var kernelArguments = new KernelArguments()
        //{
        //    {"topic", topic }
        //};

        //var title = await kernel.InvokeAsync(plugin["GenerateTitle"], kernelArguments);
        //Console.WriteLine(title);
        //kernelArguments.Add("title", title);

        //var subtitle = await kernel.InvokeAsync(plugin["GenerateSubtitle"], kernelArguments);
        //Console.WriteLine(subtitle);
        //kernelArguments.Add("subtitle", subtitle);

        //var content = await kernel.InvokeAsync(plugin["GenerateContent"], kernelArguments);
        //Console.WriteLine(content);
        //kernelArguments.Add("content", content);

        //var imageDescription = await kernel.InvokeAsync(plugin["GenerateImageDescription"], kernelArguments);
        //Console.WriteLine(imageDescription);
        //kernelArguments.Add("imageDescription", imageDescription);

        //var imageGenerator = new ImageGenerator1();
        //var imageGeneratorPlugin = kernel.ImportPluginFromObject(imageGenerator);

        //var imageUrl = await kernel.InvokeAsync(imageGeneratorPlugin["GenerateImage"], kernelArguments);
        //Console.WriteLine(imageUrl);
        //kernelArguments.Add("imageUrl", imageUrl);

        //var publishArtcle = new PublishArtcle1();
        //var publishArtclePlugin = kernel.ImportPluginFromObject(publishArtcle);

        //await kernel.InvokeAsync(publishArtclePlugin["WriteToFileSystem"], kernelArguments);




        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GENERATE AND EXECUTE PLAN
        //        Console.Write("Please enter the request: ");
        //        string? request = Console.ReadLine();

        //        kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteArticle");
        //        var imageGenerator = new ImageGenerator1();
        //        kernel.ImportPluginFromObject(imageGenerator);
        //        var articlePublisher = new PublishArtcle1();
        //        kernel.ImportPluginFromObject(articlePublisher);

        //#pragma warning disable SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        //        var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });
        //        var plan = await planner.CreatePlanAsync(kernel, request);
        //        Console.WriteLine(plan.ToString());
        //        var result = await plan.InvokeAsync(kernel);
        //#pragma warning restore SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        //        Console.WriteLine("Result:");
        //        Console.WriteLine(result);
        //        Console.ReadLine();








        // /++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  CHAT SERVICE
        IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        // Create the chat history
        ChatHistory chatMessages = new ChatHistory("""
                    You are a friendly assistant who likes to follow the rules. You will complete required steps
                    and request approval before taking any consequential actions. If the user doesn't provide
                    enough information for you to complete a task, you will keep asking questions until you have
                    enough information to complete the task.
                    """);


        kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteArtcle");
        kernel.Plugins.AddFromType<ImageGenerator>();
        kernel.Plugins.AddFromType<PublishArtcle>();


        // Start the conversation
        while (true)
        {
            // Get user input
            Console.Write("User > ");
            chatMessages.AddUserMessage(Console.ReadLine()!);

            // Get the chat completions
            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
                chatMessages,
                executionSettings: openAIPromptExecutionSettings,
                kernel: kernel);

            // Stream the results
            string fullMessage = "";
            await foreach (var content in result)
            {
                if (content.Role.HasValue && !string.IsNullOrEmpty(content?.Content))
                {
                    Console.Write("Assistant > ");
                }
                Console.Write(content.Content);
                fullMessage += content.Content;
            }
            Console.WriteLine();

            // Add the message from the agent to the chat history
            chatMessages.AddAssistantMessage(fullMessage);
        }

    }
}