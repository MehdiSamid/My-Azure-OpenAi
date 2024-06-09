//using Azure;
//using Azure.AI.OpenAI;
//using Azure.Core;
//using Microsoft.AspNetCore.Mvc;
//using OpenAI_UIR.Models;
//using System;

//namespace YourProject.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class OpenAIController : ControllerBase
//    {
//        private readonly OpenAIClient _openAIClient;

//        public OpenAIController(OpenAIClient openAIClient)
//        {
//            _openAIClient = openAIClient;
//        }

//        [HttpPost]
//        public async Task<ActionResult<string>> GenerateResponse([FromBody] string userInput)
//        {
//            // Check if API key is not null
//            string apiKey = "c2c5da4808944d9c919071dceb1075f3";// Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
//            if (string.IsNullOrEmpty(apiKey))
//            {
//                return BadRequest("Azure OpenAI API key is not configured.");
//            }

//            // Initialize OpenAI client
//            var openAIClient = new OpenAIClient(
//                new Uri("https://zonetolearn.openai.azure.com/"),
//                new AzureKeyCredential(apiKey));

//            // Generate response using Azure OpenAI
//            var response = await openAIClient.GetChatCompletionsAsync("namodaj",

//                new ChatCompletionsOptions()
//                {Messages = {
//                    //new ChatMessage(ChatRole.System, "salam"),
//                    //new ChatMessage(ChatRole.User, @"hi*"),
//new ChatMessage(ChatRole.Assistant, userInput),

//                    },
//                    Temperature = (float)0.7,
//                    MaxTokens = 800,
//                    NucleusSamplingFactor = (float)0.95,
//                    FrequencyPenalty = 0,
//                    PresencePenalty = 0,
//                });

//            // Get the response text
//            var responseText = response.Value.Choices.First();

//            return Ok(responseText);
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure;
using Azure.AI.OpenAI;
using System;
using System.Linq;
using System.Threading.Tasks;
using OpenAI_UIR.Models;
using OpenAI_UIR.Data; // Adjust namespace based on your project structure
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class OpenAIController : ControllerBase
{
    private readonly OpenAIClient _openAIClient;
    private readonly ConversationContextDb _context;
    private readonly IConfiguration _configuration;


    public OpenAIController(OpenAIClient openAIClient, ConversationContextDb context , IConfiguration configuration)
    {
        _openAIClient = openAIClient;
        _context = context;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult<string>> GenerateResponse([FromBody] UserInputModel inputModel)
    {
        string apiKey = _configuration["AzureOpenAI:ApiKey"]; // Fetch securely in production
        if (string.IsNullOrEmpty(apiKey))
        {
            return BadRequest("Azure OpenAI API key is not configured.");
        }

        // Initialize OpenAI client
        var openAIClient = new OpenAIClient(
            new Uri("https://zonetolearn.openai.azure.com/"),
            new AzureKeyCredential(apiKey));

        // Determine the user to associate with the conversation
        User user = null;
        if (inputModel.UserId.HasValue)
        {
            user = await _context.Users.FindAsync(inputModel.UserId.Value);
        }
        if (user == null)
        {
            // Use the "Anonymous" user if no specific user is found
            user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "Anonymous");
            if (user == null)
            {
                return BadRequest("Anonymous user not found. Please ensure it is seeded in the database.");
            }
        }

        // Find existing conversation or create a new one
        Conversation conversation;
        if (inputModel.ConversationId.HasValue)
        {
            conversation = await _context.Conversations
                .Include(c => c.Questions)
                .ThenInclude(q => q.Responses)
                .FirstOrDefaultAsync(c => c.Id == inputModel.ConversationId.Value);

            if (conversation == null)
            {
                // Create a new conversation if not found
                conversation = new Conversation
                {
                    CreatedAt = DateTime.UtcNow,
                    UserId = user.Id
                };
                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            conversation = new Conversation
            {
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
        }

        // Retrieve conversation history
        var conversationHistory = await _context.Questions
            .Include(q => q.Responses)
            .Where(q => q.ConversationId == conversation.Id)
            .ToListAsync();

        // Concatenate previous questions and responses to form context
        var contextBuilder = new StringBuilder();
        foreach (var item in conversationHistory)
        {
            contextBuilder.AppendLine($"Q: {item.QuestionContent}");
            foreach ( var resp in item.Responses)
            {
                contextBuilder.AppendLine($"A: {resp.Message}");
            }
        }
        var context = contextBuilder.ToString();

        // Create a new question entity
        var question = new Question
        {
            QuestionContent = inputModel.UserInput,
            CreatedAt = DateTime.UtcNow,
            ConversationId = conversation.Id
        };

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        // Generate response using Azure OpenAI
        var response = await openAIClient.GetChatCompletionsAsync("namodaj",
            new ChatCompletionsOptions()
            {
                Messages = {
                    new ChatMessage(ChatRole.Assistant, context + inputModel.UserInput),
                },
                Temperature = (float)0.7,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
            });

        var responseText = response.Value.Choices.First().Message.Content;

        // Create a new response entity
        var responseEntity = new OpenAI_UIR.Models.Response
        {
            Message = responseText,
            QuestionId = question.Id,
            CreatedAt = DateTime.UtcNow
        };

        _context.Responses.Add(responseEntity);
        await _context.SaveChangesAsync();

        return Ok(new { ConversationId = conversation.Id, Response = responseText });
    }


}
