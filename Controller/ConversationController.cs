using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Data;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Controller
{
    public class ConversationController : ControllerBase
    {
        private readonly ConversationContextDb _context;

        public ConversationController(ConversationContextDb context)
        {
            _context = context;
        }

        // GET: api/Conversation/{id}
        [HttpGet("Conversation/{id}")]
        public async Task<ActionResult<Conversation>> GetConversationById(int id)
        {
            var conversation = await _context.Conversations
                .Include(c => c.Questions)
                    .ThenInclude(q => q.Responses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conversation == null)
            {
                return NotFound();
            }

            return Ok(conversation);
        }

        // GET: api/Conversation/{id}
        [HttpGet("Conversation/IdUser/{id}")]
        public async Task<ActionResult<Conversation>> GetConversationByIdUser(int id)
        {
            var conversation = await _context.Conversations
                .Include(c => c.Questions)
                    .ThenInclude(q => q.Responses)
                .Where(c => c.UserId == id).ToListAsync();

            if (conversation == null)
            {
                return NotFound();
            }

            return Ok(conversation);
        }

        // GET: api/Conversation
        [HttpGet("Conversation/GetAll")]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetAllConversations()
        {
            var conversations = await _context.Conversations
                .Include(c => c.Questions)
                    .ThenInclude(q => q.Responses)
                .ToListAsync();

            return Ok(conversations);
        }

        // GET: api/Conversation/{id}/QuestionsAndResponses
        [HttpGet("Conversation/{id}/QuestionsAndResponses")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsAndResponsesByConversationId(int id)
        {
            var questions = await _context.Questions
                .Where(q => q.ConversationId == id)
                .Include(q => q.Responses)
                .ToListAsync();

            if (questions == null || !questions.Any())
            {
                return NotFound();
            }

            return Ok(questions);
        }
    }
}
