using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string questionContent { get; set; }

        public Response Response { get; set; }

        public Conversation Conversation { get; set; }

        [ForeignKey("Conversation")]
        public int Id_Conversation { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
