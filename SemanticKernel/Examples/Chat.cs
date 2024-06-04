using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;


namespace SemanticKernel.Examples
{
    public class Chat
    {
        private readonly Kernel _kernel;
        public Chat(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Execute()
        {
            var getIntent = _kernel.CreateFunctionFromPrompt(
            new()
            {
                Template = Prompts.Prompts.AskIntent_ProvideOptions_Structure_Context_Roles_Template,
                TemplateFormat = "handlebars"
            },
            new HandlebarsPromptTemplateFactory()
        );

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
        new ChatMessageContent(AuthorRole.User, "Can you schedule a movie so I can watch it later ?"),
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

                // Create chat function 
                var chat = _kernel.CreateFunctionFromPrompt(
                   @"{{ConversationSummaryPlugin.SummarizeConversation $history}}
            User: {{$request}}
            Assistant: ");

                // Create function to determine intent of user
                var intent = await _kernel.InvokeAsync(
                getIntent,
                 new()
                    {
            { "request", request },
            { "choices", choices },
            { "history", history },
            { "fewShotExamples", fewShotExamples }
                    }
                    );

                // End the chat if the intent is "Stop"
                if (intent.ToString() == "EndConversation")
                {
                    break;
                }

                // Get chat response
                var chatResult = _kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                    chat,
                    new()
                    {
            { "request", request },
            { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
                    }
                );


                // Stream the response
                string message = "";
                await foreach (var chunk in chatResult)
                {
                    if (chunk.Role.HasValue)
                    {
                        Console.Write(chunk.Role + " > ");
                    }

                    message += chunk;
                    Console.Write(chunk);
                }
                Console.WriteLine();

                // Append to history
                history.AddUserMessage(request!);
                history.AddAssistantMessage(message);
            }

        }
    }
}
