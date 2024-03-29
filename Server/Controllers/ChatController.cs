﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.Chat;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.Record;
using WheelOfFortune.Shared.Model.User;
using WheelOfFortune.Shared.ViewModel;

namespace WheelOfFortune.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ChatController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetChats()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var result = await _context.Chats
                .Where(c => c.Users.Where(u => u.Id == userId).Count() > 0)
                .Select(c => new ChatModel()
                {
                    Id = c.Id,
                    User = _mapper.Map<ReadUserDto>(c.Users.First(u => u.Id != userId)),
                    RealEstate = _mapper.Map<ReadRealEstateDto>(c.RealEstate),

                }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("{realEstateId:int}")]
        [Authorize]
        public async Task<IActionResult> CreateChat(int realEstateId)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var findedChat = await _context.Chats.FirstOrDefaultAsync(c => c.RealEstateId == realEstateId & c.Users.Any(u => u.Id == userId));
            if (findedChat != null)
            {
                return Ok(findedChat.Id);
            }
            var findedRealEstate = await _context.RealEstates.Include(r => r.Landlord).FirstOrDefaultAsync(r => r.Id == realEstateId);
            if (findedRealEstate == null)
            {
                return NotFound("RealEstate is missing");
            }
            var newChat = new ChatEntity()
            {
                RealEstateId = findedRealEstate.Id,
                Users = new List<UserEntity>()
                {
                    _context.Users.First(u => u.Id == userId),
                    findedRealEstate.Landlord
                }
            };
            try
            {
                await _context.Chats.AddAsync(newChat);
                await _context.SaveChangesAsync();
                return Ok(newChat.Id);

            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }




        [HttpGet("{chatId:int}")]
        [Authorize]
        public async Task<IActionResult> GetChatMessages(int chatId)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var findedChat = await _context.Chats.Include(c => c.Users).Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
            if (findedChat == null)
            {
                return NotFound();
            }
            if (!findedChat.Users.Any(u => u.Id == userId))
            {
                return Forbid();
            }
            var result = _mapper.Map<ICollection<MessageModel>>(findedChat.Messages);
            return Ok(result);

        }

    }
}
