using AutoMapper;
using Domain.Models.GroupChat;
using Repository.Entity;

namespace NetCoreChatRoomAPI.Extensions.AutoMapper
{
    public class GroupChatProfile : Profile
    {
        public GroupChatProfile()
        {
            CreateMap<GroupChatModel, GroupChatEntity>();
            CreateMap<GroupChatEntity, GroupChatModel>();
        }
    }
}