using System.ComponentModel.DataAnnotations;

namespace Domain.InputModel.GroupChatMessage
{
    public class GetLastGroupChatMessageInputModel
    {
        [Required(ErrorMessage = "GroupChat code is required")]
        public int CodGroupChat { get; set; }

        [Required(ErrorMessage = "Return quantity is required")]
        public int Quantity { get; set; }
    }
}