using AutoMapper;
using Domain.InputModel.GroupChat;
using Domain.Models.GroupChat;
using Domain.Services.Interfaces;
using Repository.Entity;
using Repository.UnitOfWork;
using System.Transactions;

namespace Domain
{
    public class GroupChatDomain
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISignalRService _signarR;

        public GroupChatDomain(IUnitOfWork uow, IMapper mapper, ISignalRService signalR)
        {
            _uow = uow;
            _mapper = mapper;
            _signarR = signalR;
        }

        public List<GroupChatModel> GetAllGroupChat()
        {
            var groupList = _uow.GroupChatRepository.GetAll();
            return _mapper.Map<List<GroupChatModel>>(groupList);
        }

        public bool CreateGroupChat(CreateGroupChatInputModel inputModel)
        {
            var groupChatEntity = new GroupChatEntity(inputModel.Name, inputModel.Description, inputModel.CreatedByUser);

            using (TransactionScope ts = new TransactionScope())
            {
                groupChatEntity = _uow.GroupChatRepository.Create(groupChatEntity);
                _signarR.CreateGroupChat(inputModel.ConnectionId, groupChatEntity.Id.ToString());

                var groupChatMessageEntity = new GroupChatMessageEntity(groupChatEntity.Id, groupChatEntity.CreatedByUser, $"Group created by {groupChatEntity.CreatedByUser}");
                _uow.GroupChatMessageRepository.Create(groupChatMessageEntity);

                _signarR.SendMessageToGroup(groupChatEntity.Id.ToString(), groupChatEntity.CreatedByUser, $"Group created by {groupChatEntity.CreatedByUser}");
                ts.Complete();
            }

            return true;
        }

        public void AddUserToGroupChat(string connectionId, string groupName) => _signarR.CreateGroupChat(connectionId, groupName);
    }
}