using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.User;
using WheelOfFortune.Shared.ViewModel;

namespace WheelOfFortune.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly DatabaseContext _context;
        public ChatHub(DatabaseContext context)
        {
            _context = context;
        }

        public static ConcurrentDictionary<string, UserEntity> Users = new();

        public override Task OnConnectedAsync()
        {
            var userId = int.Parse(Context.User.Claims.First(c => c.Type == "Sub").Value);
            Users.TryAdd(Context.ConnectionId, _context.Users.First(u => u.Id == userId));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Users.TryRemove(Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(MessageModel message)
        {

            await Clients.Client("").SendAsync("ReceiveMessage", message);
        }



    }
}
