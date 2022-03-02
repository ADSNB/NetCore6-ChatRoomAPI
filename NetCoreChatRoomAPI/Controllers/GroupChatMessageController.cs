using Domain;
using Domain.InputModel.GroupChatMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreChatRoomAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class GroupChatMessageController : ControllerBase
    {
        private readonly GroupChatMessageDomain _domain;

        public GroupChatMessageController(GroupChatMessageDomain domain) => _domain = domain;

        [HttpPost]
        public async Task<IActionResult> SendMessage(string user, string message)
        {
            try
            {
                await _domain.SendMessage(user, message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetLastGroupChatMessage(GetLastGroupChatMessageInputModel inputModel)
        {
            try
            {
                var lastMessages = _domain.GetLastGroupChatMessage(inputModel);
                return Ok(lastMessages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SendMessageToGroup(SendMessageToGroupInputModel inputModel)
        {
            try
            {
                _domain.SendMessageToGroup(inputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}