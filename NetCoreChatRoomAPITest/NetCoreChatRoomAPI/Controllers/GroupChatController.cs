using Domain.Interfaces;
using Domain.Models.GroupChat;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreChatRoomAPI.Controllers;
using NuGet.Protocol;
using OfficeOpenXml.ConditionalFormatting;
using Repository.UnitOfWork;

namespace NetCoreChatRoomAPITest.NetCoreChatRoomAPI.Controllers
{
    public class GroupChatController
    {
        private Mock<IGroupChatDomain> _groupChatDomainMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public GroupChatController()
        {
            _groupChatDomainMock = new Mock<IGroupChatDomain>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void GetAllGroupChat()
        {
            // 1. Arrange
            List<GroupChatModel> groupChat = new List<GroupChatModel>
            {
                new GroupChatModel {
                    Id = 1,
                    Description = "description",
                    LastReceivedMessage = DateTime.Now.ToString(),
                    Name = "name",
                },
                new GroupChatModel {
                    Id = 2,
                    Description = "description 2",
                    LastReceivedMessage = DateTime.Now.ToString(),
                    Name = "name 2",
                }
            };

            // return type of groupChat GetAllGroupChat() method
            _groupChatDomainMock.Setup(um => um.GetAllGroupChat()).Returns(groupChat);

            // define controller using mocked service
            global::NetCoreChatRoomAPI.Controllers.GroupChatController groupChatController = new global::NetCoreChatRoomAPI.Controllers.GroupChatController(_groupChatDomainMock.Object);

            // 2. Act
            var controllerResponse = groupChatController.GetAllGroupChat();

            // 3. Assert
            Assert.IsType<OkObjectResult>(controllerResponse);

            var okObjectResult = controllerResponse as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<GroupChatModel>;
            Assert.NotNull(model);

            var actual = model;
            Assert.Equal(groupChat, actual);
        }
    }
}