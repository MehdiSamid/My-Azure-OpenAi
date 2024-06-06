using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public DateTime CreatedAt { get; set; }
    }





}
