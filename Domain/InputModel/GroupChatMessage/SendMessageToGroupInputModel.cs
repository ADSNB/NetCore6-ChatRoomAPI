using System.ComponentModel.DataAnnotations;

namespace Domain.InputModel.GroupChatMessage
{
    public class SendMessageToGroupInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "User ConnectionId is required")]
        public string ConnectionId { get; set; }

        [Required(ErrorMessage = "GroupChat code is required")]
        public int CodGroupChat { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FromUser is required")]
        public string FromUser { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Message is required")]
        public string Message { get; set; }

        public bool IsRobotProcessingCall { get; set; }
    }
}