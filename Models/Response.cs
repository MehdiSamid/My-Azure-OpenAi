using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    [Table("Responses")]
    public class Response
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; } = string.Empty;

        [ForeignKey("Question")]
        public int QuestionId { get; set; }  // Foreign key to Question

        public Question Question { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
