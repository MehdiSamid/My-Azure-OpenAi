using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    public class Response
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!; // Using null-forgiving operator

        public DateTime CreatedAt { get; set; }
    }
    
}
