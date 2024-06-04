using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernel.Examples
{
    public class ChatWithNativeFunctions
    {
        private readonly Kernel _kernel;
        public ChatWithNativeFunctions(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Execute()
        {
            // Retrieve the chat completion service from the kernel
            IChatCompletionService chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

            // Create the chat history
            ChatHistory chatMessages = new ChatHistory("""
            You are a friendly assistant who likes to follow the rules. You will complete required steps
            and request approval before taking any consequential actions. If the user doesn't provide
            enough information for you to complete a task, you will keep asking questions until you have
            enough information to complete the task.
            """);

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
                    kernel: _kernel);

                // Stream the results
                string fullMessage = "";
                await foreach (var content in result)
                {
                    if (content.Role.HasValue && !string.IsNullOrEmpty(content.Content))
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
}
