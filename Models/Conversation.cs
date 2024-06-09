using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    [Table("Conversations")]
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid GuidId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }





}
