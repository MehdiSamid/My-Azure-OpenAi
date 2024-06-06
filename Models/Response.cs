using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    public class Response
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("Question")]
        public int Id_Question { get; set; }
        public Question Question { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
