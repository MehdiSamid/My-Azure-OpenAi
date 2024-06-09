using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string QuestionContent { get; set; } = string.Empty;

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }  // Foreign key to Conversation

        public Conversation Conversation { get; set; }

        public ICollection<Response> Responses { get; set; } = new List<Response>();

        public DateTime CreatedAt { get; set; }
    }

}
