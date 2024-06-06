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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            return await _context.Conversation.ToListAsync();
        }

        [HttpGet("conversation/{id}")]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
            var conversation = await _context.Conversation.FindAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }

            return conversation;
        }

        [HttpPost]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            _context.Conversation.Add(conversation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConversation), new { id = conversation.Id }, conversation);
        }

        [HttpPut("conversation/{id}")]
        public async Task<IActionResult> PutConversation(int id, Conversation conversation)
        {
            if (id != conversation.Id)
            {
                return BadRequest();
            }

            _context.Entry(conversation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("conversation/{id}")]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            _context.Conversation.Remove(conversation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversation.Any(e => e.Id == id);
        }
    }
}
