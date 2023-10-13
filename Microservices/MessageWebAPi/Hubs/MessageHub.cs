using AutoMapper;
using ChatApplication.Db;
using Microsoft.AspNetCore.SignalR;

namespace MessageWebAPi.Hubs
{
    public class MessageHub : Hub
    {
        public static Dictionary<string, List<string>> connectedClients = new Dictionary<string, List<string>>();
        private DataContext _context;
        private readonly IMapper _mapper;
        public MessageHub(DataContext context,IMapper mapper){
            _context = context;
            _mapper = mapper;
        } 

        

        public async Task SendMessage(object message, string roomName)
        {
            await EmitLog("Client " + Context.ConnectionId + " said: " + message, roomName);

            await Clients.OthersInGroup(roomName).SendAsync("message", message);
        }

        public async Task JoinRoom(string roomName)
        {
            await EmitLog("Received request to create or join room " + roomName + " from a client " + Context.ConnectionId, roomName);

            if (!connectedClients.ContainsKey(roomName))
            {
                connectedClients.Add(roomName, new List<string>());
            }

            if (!connectedClients[roomName].Contains(Context.ConnectionId))
            {
                connectedClients[roomName].Add(Context.ConnectionId);
            }

            await EmitJoinRoom(roomName);

            var numberOfClients = connectedClients[roomName].Count;

            if (numberOfClients == 1)
            {
                await EmitCreated();
                await EmitLog("Client " + Context.ConnectionId + " created the room " + roomName, roomName);
            }
            else
            {
                await EmitJoined(roomName);
                await EmitLog("Client " + Context.ConnectionId + " joined the room " + roomName, roomName);
            }

            await EmitLog("Room " + roomName + " now has " + numberOfClients + " client(s)", roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await EmitLog("Received request to leave the room " + roomName + " from a client " + Context.ConnectionId, roomName);

            if (connectedClients.ContainsKey(roomName) && connectedClients[roomName].Contains(Context.ConnectionId))
            {
                connectedClients[roomName].Remove(Context.ConnectionId);
                await EmitLog("Client " + Context.ConnectionId + " left the room " + roomName, roomName);

                if (connectedClients[roomName].Count == 0)
                {
                    connectedClients.Remove(roomName);
                    await EmitLog("Room " + roomName + " is now empty - resetting its state", roomName);
                }
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        private async Task EmitJoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        private async Task EmitCreated()
        {
            await Clients.Caller.SendAsync("created");
        }

        private async Task EmitJoined(string roomName)
        {
            await Clients.Group(roomName).SendAsync("joined");
        }

        private async Task EmitLog(string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("log", "[Server]: " + message);
        }
    }
}
