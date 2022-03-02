using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Domain.Services
{
    public class SignalRService : Hub, ISignalRService
    {
        private readonly HubConnection _connection;

        public SignalRService(AppSettingsModel appSettings)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(appSettings.SignalR.BaseUrl + appSettings.SignalR.HubName)
                .Build();
        }

        private async Task CreateConnection() => await _connection.StartAsync();

        private async Task DisposeConnection() => await _connection.DisposeAsync();

        public async Task SendMessage(string user, string message)
        {
            await CreateConnection();
            await _connection.InvokeAsync("ISendMessage", user, message);
            await DisposeConnection();
        }

        public async Task CreateGroupChat(string connectionId, string codGroupChat)
        {
            await CreateConnection();
            await _connection.InvokeAsync("IJoinRoom", connectionId, codGroupChat);
            await DisposeConnection();
        }

        public async Task SendMessageToGroup(string codGroupChat, string fromUser, string message)
        {
            await CreateConnection();
            await _connection.InvokeAsync("ISendMessageToGroup", codGroupChat, fromUser, message);
            await DisposeConnection();
        }

        public async Task ISendMessage(string user, string message) => await Clients.All.SendAsync("ReceiveMessage", user, message);

        public async Task ISendMessageToCaller(string user, string message) => await Clients.Caller.SendAsync("ReceiveMessage", user, message);

        public async Task ISendMessageToGroup(string groupName, string user, string message) => await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);

        public async Task IJoinRoom(string connectionId, string roomName) => await Groups.AddToGroupAsync(connectionId.Length > 0 ? connectionId : Context.ConnectionId, roomName);

        //public async Task IJoinRoom(string connectionId, string roomName) => await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        public async Task ILeaveRoom(string connectionId, string roomName) => await Groups.RemoveFromGroupAsync(connectionId, roomName);

        //public async Task ILeaveRoom(string connectionId, string roomName) => await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }
}