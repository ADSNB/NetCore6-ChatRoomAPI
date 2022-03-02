using Domain;
using Domain.InputModel.GroupChat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace NetCoreChatRoomAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class GroupChatController : ControllerBase
    {
        public GroupChatDomain _domain;

        public GroupChatController(GroupChatDomain domain) => _domain = domain;

        [HttpGet]
        public IActionResult GetAllGroupChat()
        {
            try
            {
                var groups = _domain.GetAllGroupChat();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                Log.Error(JsonConvert.SerializeObject(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateGroupChat(CreateGroupChatInputModel inputModel)
        {
            try
            {
                var newGroup = _domain.CreateGroupChat(inputModel);
                return Ok(newGroup);
            }
            catch (Exception ex)
            {
                Log.Error(JsonConvert.SerializeObject(ex));
                return BadRequest(ex.Message);
            }
        }
    }
}