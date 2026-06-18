using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace apiv2.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> ConnectionNames = new();
        private static readonly ConcurrentDictionary<string, HashSet<string>> RoomMembers = new();

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name ?? Context.ConnectionId;
            ConnectionNames[Context.ConnectionId] = userName;

            await Clients.All.SendAsync("UserConnected", new
            {
                ConnectionId = Context.ConnectionId,
                UserName = userName
            });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (ConnectionNames.TryRemove(Context.ConnectionId, out var userName))
            {
                await Clients.All.SendAsync("UserDisconnected", new
                {
                    ConnectionId = Context.ConnectionId,
                    UserName = userName
                });
            }

            foreach (var room in RoomMembers.Keys)
            {
                if (RoomMembers.TryGetValue(room, out var members) && members.Remove(Context.ConnectionId))
                {
                    await Clients.Group(room).SendAsync("RoomUserLeft", room, userName, Context.ConnectionId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public Task SendPublicMessage(string message)
        {
            var sender = Context.User?.Identity?.Name ?? Context.ConnectionId;
            return Clients.All.SendAsync("ReceivePublicMessage", new
            {
                Sender = sender,
                Message = message,
                SentAt = DateTime.UtcNow
            });
        }

        public Task SendPrivateMessage(string connectionId, string message)
        {
            var sender = Context.User?.Identity?.Name ?? Context.ConnectionId;
            return Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", new
            {
                Sender = sender,
                Message = message,
                SentAt = DateTime.UtcNow
            });
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            var room = RoomMembers.GetOrAdd(roomName, _ => new System.Collections.Generic.HashSet<string>());
            lock (room)
            {
                room.Add(Context.ConnectionId);
            }

            await Clients.Group(roomName).SendAsync("RoomUserJoined", roomName, Context.User?.Identity?.Name ?? Context.ConnectionId, Context.ConnectionId);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

            if (RoomMembers.TryGetValue(roomName, out var room))
            {
                lock (room)
                {
                    room.Remove(Context.ConnectionId);
                }
            }

            await Clients.Group(roomName).SendAsync("RoomUserLeft", roomName, Context.User?.Identity?.Name ?? Context.ConnectionId, Context.ConnectionId);
        }

        public Task SendRoomMessage(string roomName, string message)
        {
            var sender = Context.User?.Identity?.Name ?? Context.ConnectionId;
            return Clients.Group(roomName).SendAsync("ReceiveRoomMessage", new
            {
                Room = roomName,
                Sender = sender,
                Message = message,
                SentAt = DateTime.UtcNow
            });
        }

        public Task GetConnectedUsers()
        {
            var users = ConnectionNames.Select(kvp => new
            {
                ConnectionId = kvp.Key,
                UserName = kvp.Value
            }).ToArray();

            return Clients.Caller.SendAsync("ConnectedUsers", users);
        }
    }
}