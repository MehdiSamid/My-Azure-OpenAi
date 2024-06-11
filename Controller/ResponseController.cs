//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OpenAI_UIR.Data;
//using OpenAI_UIR.Models;

//namespace OpenAI_UIR.Controller
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ResponseController : ControllerBase
//    {
//        private readonly ConversationContextDb _context;

//        public ResponseController(ConversationContextDb context)
//        {
//            _context = context;
//        }

//        // GET: api/Response
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Response>>> GetResponses()
//        {
//            return await _context.Responses.ToListAsync();
//        }

//        // GET: api/Response/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Response>> GetResponse(int id)
//        {
//            var response = await _context.Responses.FindAsync(id);

//            if (response == null)
//            {
//                return NotFound();
//            }

//            return response;
//        }

//        // POST: api/Response
//        [HttpPost]
//        public async Task<ActionResult<Response>> PostResponse(Response response)
//        {
//            _context.Responses.Add(response);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetResponse", new { id = response.Id }, response);
//        }

//        // PUT: api/Response/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutResponse(int id, Response response)
//        {
//            if (id != response.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(response).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ResponseExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // DELETE: api/Response/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteResponse(int id)
//        {
//            var response = await _context.Responses.FindAsync(id);
//            if (response == null)
//            {
//                return NotFound();
//            }

//            _context.Responses.Remove(response);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ResponseExists(int id)
//        {
//            return _context.Responses.Any(e => e.Id == id);
//        }
//    }
//}