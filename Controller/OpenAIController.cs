//using Microsoft.AspNetCore.Mvc;
//using Azure.AI.OpenAI;
//using Azure.Core;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Azure;

//namespace YourProject.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class OpenAIController : ControllerBase
//    {
//        [HttpPost]
//        public async Task<ActionResult<string>> GenerateResponse([FromBody] string userInput)
//        {
//            // Initialize OpenAI client
//            var openAIClient = new OpenAIClient(
//                new Uri("https://YOUR_OPENAI_ENDPOINT"),
//                new AzureKeyCredential(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")));

//            // Generate response using Azure OpenAI
//            var response = await openAIClient.GetChatCompletionsAsync(
//                userInput,
//                new ChatCompletionsOptions()
//                {
//                    Temperature = (float)0.7,
//                    MaxTokens = 800,
//                    NucleusSamplingFactor = (float)0.95,
//                    FrequencyPenalty = 0,
//                    PresencePenalty = 0,
//                });

//            // Get the response text
//            var responseText = response.Value.Choices.FirstOrDefault();

//            return Ok(responseText);
//        }
//    }
//}

using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Models;
using System;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly OpenAIClient _openAIClient;

        public OpenAIController(OpenAIClient openAIClient)
        {
            _openAIClient = openAIClient;
        }

        [HttpPost]
        public async Task<ActionResult<string>> GenerateResponse([FromBody] string userInput)
        {
            // Check if API key is not null
            string apiKey = "c2c5da4808944d9c919071dceb1075f3";// Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Azure OpenAI API key is not configured.");
            }

            // Initialize OpenAI client
            var openAIClient = new OpenAIClient(
                new Uri("https://zonetolearn.openai.azure.com/"),
                new AzureKeyCredential(apiKey));

            // Generate response using Azure OpenAI
            var response = await openAIClient.GetChatCompletionsAsync("namodaj",
                
                new ChatCompletionsOptions()
                {Messages = {
                    //new ChatMessage(ChatRole.System, "salam"),
                    //new ChatMessage(ChatRole.User, @"hi*"),
new ChatMessage(ChatRole.Assistant, userInput),

                    },
                    Temperature = (float)0.7,
                    MaxTokens = 800,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            // Get the response text
            var responseText = response.Value.Choices.First();

            return Ok(responseText);
        }
    }
}

