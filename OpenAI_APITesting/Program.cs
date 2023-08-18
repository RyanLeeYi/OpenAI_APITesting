using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3;

namespace OpenAI_APITesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = "";
            Start:
            Console.WriteLine("請輸入你要問的問題");

            string inputString = Console.ReadLine() ?? "What is the meaning of life?";

            var gpt3 = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = apiKey
            });


            var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = inputString,
                Model = Models.TextDavinciV2,
                Temperature = 0.5F,
                MaxTokens = 100,
                N = 3
            });

            if (completionResult.Successful)
            {
                foreach (var choice in completionResult.Choices)
                {
                    Console.WriteLine(choice.Text);
                }
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
            Console.WriteLine("再問一個問題?Y/N");
            if (string.Compare("Y",Console.ReadLine()) == 0)
            {
                goto Start;
            }
        }
    }
}