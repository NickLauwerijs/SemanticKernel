
namespace SemanticKernel.Prompts
{
    public static class Prompts
    {
        // ask intent 
        public const string AskIntent = "What is the intent of this request? {0}";

        /// <summary>
        /// Ask intent and provide options so that the model can return a predictable response ,
        /// result is more predictable but is sometimes not parsable as the model sometimes returns the option in a sentence.
        /// </summary>
        public const string AskIntent_ProvideOptions = @$"{AskIntent}
                                                           You can choose between ScheduleMovie, AddMovie";

        /// <summary>
        /// When providing structure to the prompt, the result is more likely to be just the intent instead of a sentence.
        /// This is because the LLMs have the tendency to generate text that looks like the prompt.
        /// </summary>
        public const string AskIntent_ProvideOptions_Structure = @"Instructions: What is the intent of this request?
                                                                        Choices: AddMovie, ScheduleMovie.
                                                                        User Input: {0}
                                                                        Intent: ";

        /// <summary>
        /// We can also ask the model to return the result in a specific json format which will be much more usable inside our application
        /// </summary>
        public const string AskIntent_ProvideOptions_Structure_Json = @"Instructions: What is the intent of this request?
                                                                        Please return the result in following format : 
                                                                        {{
                                                                            intent: {{intent}}
                                                                        }}
                                                                        Choices: AddMovie, ScheduleMovie.
                                                                        User Input: {0}
                                                                        Intent: ";

        /// <summary>
        /// For more complex scenarios it's also possible to give some example to the model so it can use it to provide a better result
        /// This is called few-shot prompting.
        /// </summary>
        public const string AskIntent_ProvideOptions_Structure_FewShot = @"Instructions: What is the intent of this request?
                                                                        Choices: AddMovie, ScheduleMovie.
                                                                        
                                                                        User Input: Can you add a movie to my watchlist?
                                                                        Intent: AddMovie
                                                                        
                                                                        User Input: Can you schedule a movie so I can watch it later ? 
                                                                        Intent: ScheduleMovie

                                                                        User Input: {0}
                                                                        Intent: ";

        /// <summary>
        /// Sometimes the model doesn't know what to respond, to avoid the model guessing the answer we can define what the model should respond
        /// </summary>
        public const string AskIntent_ProvideOptions_Structure_Unknown = @"Instructions: What is the intent of this request?

                                                                        If you don't know the intent, please take Unkown choice

                                                                        Choices: AddMovie, ScheduleMovie, Unkown.

                                                                        User Input: Can you add a movie to my watchlist?
                                                                        Intent: AddMovie
                                                                        
                                                                        User Input: Can you schedule a movie so I can watch it later ? 
                                                                        Intent: ScheduleMovie

                                                                        User Input: Can you plan a movie so I can watch it later ? 
                                                                        Intent: ScheduleMovie

                                                                        User Input: {0}
                                                                        Intent: ";

        /// <summary>
        /// When prompts are becoming more complex we can add message roles so the the model knows the difference between 
        /// system instructions, User input and Model responses.
        /// In semantic kernel message tags can be used to define these message roles
        /// </summary>
        public const string AskIntent_ProvideOptions_Structure_Context_Roles = @" 
                                                                        <message role=""system"">Instructions: What is the intent of this request?
                                                                         If you don't know the intent, please take Unkown choice
                                                                         Choices: AddMovie, ScheduleMovie, Unkown.</message>
                                                                         
                                                                        <message role=""user"">Can you add a movie to my watchlist?</message>
                                                                        <message role=""system"">Intent:</message>
                                                                        <message role=""assistant"">AddMovie</message>
                                                                        
                                                                        <message role=""user"">Can you schedule a movie so I can watch it later ?</message>
                                                                        <message role=""system"">Intent:</message>
                                                                        <message role=""assistant"">ScheduleMovie</message>

                                                                        <message role=""user"">Can you plan a movie so I can watch it later ?</message>
                                                                        <message role=""system"">Intent:</message>
                                                                        <message role=""assistant"">ScheduleMovie</message>

                                                                         <message role=""user"">{0}</message>
                                                                         <message role=""system"">Intent:</message>
                                                                         ";

        /// <summary>
        /// We can templatize our prompts so that we can easily reuse it with different parameters using the HandleBars engine
        /// </summary>
        public const string AskIntent_ProvideOptions_Structure_Context_Roles_Template = """
                                                                            <message role="system">Instructions: What is the intent of this request?
                                                                            Do not explain the reasoning, just reply back with the intent. If you are unsure, reply with {{choices[2]}}.
                                                                            Choices: {{choices}}.</message>

                                                                            {{#each fewShotExamples}}
                                                                                {{#each this}}
                                                                                    <message role="{{role}}">{{content}}</message>
                                                                                {{/each}}
                                                                            {{/each}}

                                                                            {{ConversationSummaryPlugin-SummarizeConversation history}}

                                                                            <message role="user">{{request}}</message>
                                                                            <message role="system">Intent:</message>
                                                                            """;

    }
}
