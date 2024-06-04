using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel.Helpers
{
    public static class ChatHelper
    {
        public static async Task<string> HandleChatResponse(IAsyncEnumerable<StreamingChatMessageContent> output)
        {
            string message = "";
            await foreach (var chunk in output)
            {
                if (chunk.Role.HasValue)
                {
                    Console.Write(chunk.Role + " > ");
                }

                message += chunk;
                Console.Write(chunk);
            }
            return message;
        }
    }
}
