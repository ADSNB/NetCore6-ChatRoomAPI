using AutoMapper;
using Domain.Enum;
using Domain.Helpers;
using Domain.InputModel.GroupChatMessage;
using Domain.Models.GroupChatMessage;
using Domain.Services.Interfaces;
using Repository.Entity;
using Repository.UnitOfWork;
using System.Transactions;

namespace Domain
{
    public class GroupChatMessageDomain
    {
        private readonly ISignalRService _signalR;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly GroupChatDomain _groupChatDomain;
        private readonly RobotDomain _robotDomain;

        public GroupChatMessageDomain(ISignalRService signalR, IUnitOfWork uow, IMapper mapper, GroupChatDomain groupChatDomain, RobotDomain robotDomain)
        {
            _signalR = signalR;
            _uow = uow;
            _mapper = mapper;
            _groupChatDomain = groupChatDomain;
            _robotDomain = robotDomain;
        }

        public async Task SendMessage(string user, string message) => await _signalR.SendMessage(user, message);

        public List<GroupChatMessageModel> GetLastGroupChatMessage(GetLastGroupChatMessageInputModel inputModel)
        {
            var teste = _uow.GroupChatMessageRepository.GetAll();
            // the TakeLast was givin an unknow error (need more time to investigate)... thats why we have two order commands in here... still in here we are using IQueryable, so no much performence loose for now.
            var lastMessages = _uow.GroupChatMessageRepository.GetAll().Where(e => e.CodGroupChat == inputModel.CodGroupChat).OrderByDescending(p => p.Id).Take(inputModel.Quantity).OrderBy(p => p.Id);
            return _mapper.Map<List<GroupChatMessageModel>>(lastMessages);
        }

        public void SendMessageToGroup(SendMessageToGroupInputModel inputModel)
        {
            var groupChatMessageEntity = new GroupChatMessageEntity(inputModel.CodGroupChat, inputModel.FromUser, inputModel.Message);

            using (TransactionScope ts = new TransactionScope())
            {
                groupChatMessageEntity = _uow.GroupChatMessageRepository.Create(groupChatMessageEntity);
                _groupChatDomain.AddUserToGroupChat(inputModel.ConnectionId, inputModel.CodGroupChat.ToString());

                if (!inputModel.IsRobotProcessingCall)
                {
                    if (inputModel.Message.ToLower().StartsWith(EnumHelper.GetEnumDescription(CommandTypesEnum.GetStockValue).ToLower()))
                        _robotDomain.CreateProcessingQueueRequest(groupChatMessageEntity.CodGroupChat, groupChatMessageEntity.Message, groupChatMessageEntity.FromUser);
                }

                _signalR.SendMessageToGroup(groupChatMessageEntity.CodGroupChat.ToString(), groupChatMessageEntity.FromUser, groupChatMessageEntity.Message);
                ts.Complete();
            }
        }
    }
}