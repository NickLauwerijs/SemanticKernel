using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning.Handlebars;
using SemanticKernel.Plugins.WriteArtcle;

namespace SemanticKernel.Examples
{
    public class CreateArtcle
    {
        private readonly Kernel _kernel;

        public CreateArtcle(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Execute()
        {
            Console.Write("Please enter the topic: ");
            string? topic = Console.ReadLine();


            var writePlugin = _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteArtcle");
            var kernelArguments = new KernelArguments()
                        {
                            {"topic", topic}
                        };

            var title = await _kernel.InvokeAsync(writePlugin["GenerateTitle"], kernelArguments);
            kernelArguments.Add("title", title);
            Console.WriteLine(title);

            var subtitle = await _kernel.InvokeAsync(writePlugin["GenerateSubtitle"], kernelArguments);
            kernelArguments.Add("subtitle", subtitle);
            Console.WriteLine(subtitle);

            var content = await _kernel.InvokeAsync(writePlugin["GenerateContent"], kernelArguments);
            kernelArguments.Add("content", content);
            Console.WriteLine(content);

            var description = await _kernel.InvokeAsync(writePlugin["GenerateImageDescription"], kernelArguments);
            kernelArguments.Add("imageDescription", description);
            Console.WriteLine(description);

            var imageGenerator = new ImageGenerator();
            var imageGeneratorPlugin = _kernel.ImportPluginFromObject(imageGenerator);

            var imageUrl = await _kernel.InvokeAsync(imageGeneratorPlugin["GenerateImage"], kernelArguments);
            kernelArguments.Add("imageUrl", imageUrl);
            Console.WriteLine(imageUrl);

            var articlePublisher = new PublishArtcle();
            var articlePublisherPlugin = _kernel.ImportPluginFromObject(articlePublisher);

            var writeToFileSystem = await _kernel.InvokeAsync(articlePublisherPlugin["WriteToFileSystem"], kernelArguments);


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++PLANNER
            //            Console.Write("Please enter the request: ");
            //            string? request = Console.ReadLine();

            //            _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteArtcle");
            //            var imageGenerator = new ImageGenerator();
            //            _kernel.ImportPluginFromObject(imageGenerator);
            //            var articlePublisher = new PublishArtcle();
            //            _kernel.ImportPluginFromObject(articlePublisher);

            //#pragma warning disable SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            //            var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });
            //            var plan = await planner.CreatePlanAsync(_kernel, request);
            //            Console.WriteLine(plan.ToString());
            //            var result = await plan.InvokeAsync(_kernel);
            //#pragma warning restore SKEXP0060 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            //            Console.WriteLine("Result:");
            //            Console.WriteLine(result);
            //            Console.ReadLine();


            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++CHATSERVICE
            // Retrieve the chat completion service from the kernel
            //    IChatCompletionService chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

            //    // Create the chat history
            //    ChatHistory chatMessages = new ChatHistory("""
            //            You are a friendly assistant who likes to follow the rules. You will complete required steps
            //            and request approval before taking any consequential actions. If the user doesn't provide
            //            enough information for you to complete a task, you will keep asking questions until you have
            //            enough information to complete the task.
            //            """);


            //    _kernel.ImportPluginFromPromptDirectory("../../../Plugins/WriteArtcle");
            //    _kernel.Plugins.AddFromType<ImageGenerator>();
            //    _kernel.Plugins.AddFromType<PublishArtcle>();


            //    // Start the conversation
            //    while (true)
            //    {
            //        // Get user input
            //        Console.Write("User > ");
            //        chatMessages.AddUserMessage(Console.ReadLine()!);

            //        // Get the chat completions
            //        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            //        {
            //            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            //        };

            //        var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
            //            chatMessages,
            //            executionSettings: openAIPromptExecutionSettings,
            //            kernel: _kernel);

            //        // Stream the results
            //        string fullMessage = "";
            //        await foreach (var content in result)
            //        {
            //            if (content.Role.HasValue && !string.IsNullOrEmpty(content?.Content))
            //            {
            //                Console.Write("Assistant > ");
            //            }
            //            Console.Write(content.Content);
            //            fullMessage += content.Content;
            //        }
            //        Console.WriteLine();

            //        // Add the message from the agent to the chat history
            //        chatMessages.AddAssistantMessage(fullMessage);
            //    }
        }
    }
}
