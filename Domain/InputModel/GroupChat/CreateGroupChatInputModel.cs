using System.ComponentModel.DataAnnotations;

namespace Domain.InputModel.GroupChat
{
    public class CreateGroupChatInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The ConnectionId is required")]
        public string ConnectionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "GroupChat name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "GroupChat description is required")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User information is required")]
        public string CreatedByUser { get; set; }
    }
}