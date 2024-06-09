namespace OpenAI_UIR.Models
{
    public class UserInputModel
    {
        public int? UserId { get; set; }  // Add this field
        public int? ConversationId { get; set; }
        public string UserInput { get; set; } = string.Empty;
    }

}
