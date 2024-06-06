using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string QuestionContent { get; set; } = string.Empty;

        public ICollection<Response> Responses { get; set; } = new List<Response>();

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; } = null!; // Using null-forgiving operator

        public DateTime CreatedAt { get; set; }
    }

}
