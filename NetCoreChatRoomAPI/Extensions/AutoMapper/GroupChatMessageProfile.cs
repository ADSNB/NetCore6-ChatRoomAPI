using AutoMapper;
using Domain.Models.GroupChatMessage;
using Repository.Entity;

namespace NetCoreChatRoomAPI.Extensions.AutoMapper
{
    public class GroupChatMessageProfile : Profile
    {
        public GroupChatMessageProfile()
        {
            CreateMap<GroupChatMessageModel, GroupChatMessageEntity>();
            CreateMap<GroupChatMessageEntity, GroupChatMessageModel>();
        }
    }
}