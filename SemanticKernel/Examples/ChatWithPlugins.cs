using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernel.Extensions;
using SemanticKernel.Helpers;

namespace SemanticKernel.Examples
{
    public class ChatWithPlugins
    {
        private readonly Kernel _kernel;
        public ChatWithPlugins(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Execute()
        {
            // Load prompt from YAML
            KernelFunction getIntent = await _kernel.LoadFunctionFromYaml("SemanticKernel.Prompts.GetIntentFunction.prompt.yaml");

            // Create choices
            List<string> choices = ["AddMovie", "ScheduleMovie", "Unkown", "EndConversation"];

            // Create few-shot examples
            List<ChatHistory> fewShotExamples =
            [
                [
        new ChatMessageContent(AuthorRole.User, "Can you add a movie to my watchlist?"),
        new ChatMessageContent(AuthorRole.System, "Intent:"),
        new ChatMessageContent(AuthorRole.Assistant, "AddMovie")
    ],
    [
        new ChatMessageContent(AuthorRole.User, "Can you schedule a movie so I can watch it later ?"),
        new ChatMessageContent(AuthorRole.System, "Intent:"),
        new ChatMessageContent(AuthorRole.Assistant, "ScheduleMovie")
    ],
    [
        new ChatMessageContent(AuthorRole.User, "Can you plan a movie"),
        new ChatMessageContent(AuthorRole.System, "Intent:"),
        new ChatMessageContent(AuthorRole.Assistant, "ScheduleMovie")
    ]
            ];
            ChatHistory history = [];

            // Start the chat loop
            while (true)
            {
                // Get user input
                Console.Write("User > ");
                var request = Console.ReadLine();

                // Create function to determine intent of user
                var intent = _kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                getIntent,
                 new()
                    {
            { "request", request },
            { "choices", choices },
            { "history", history },
            { "fewShotExamples", fewShotExamples }
                    }
                    );

                // Stream the response
                var message = await ChatHelper.HandleChatResponse(intent);

                if (intent.ToString() == "EndConversation")
                {
                    break;
                }

                Console.WriteLine();

                // Append to history
                history.AddUserMessage(request!);
                history.AddAssistantMessage(message);
            }
        }
    }
}
