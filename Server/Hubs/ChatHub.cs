using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.Message;
using WheelOfFortune.Shared.Model.User;
using WheelOfFortune.Shared.ViewModel;

namespace WheelOfFortune.Server.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public ChatHub(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var chat = _context.Chats.Include(c => c.Users).Include(c => c.Messages).FirstOrDefault(c => c.Id == message.ChatId);
            if (chat == null)
            {
                throw new HubException("Not found chat");
            }
            chat.Messages.Add(_mapper.Map<MessageEntity>(message));
            message.FromUser = _mapper.Map<ReadUserDto>(chat.Users.First(u => u.Id == message.FromUserId));
            await _context.SaveChangesAsync();
            foreach (var item in chat.Users)
            {
                var user = Users.FirstOrDefault(u => u.Value.Id == item.Id);
                if (user.Key != null)
                {
                    await Clients.Client(user.Key).SendAsync("ReceiveMessage", message);
                }
            }
        }



    }
}
