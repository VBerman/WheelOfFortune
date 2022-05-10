﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WheelOfFortune.Server.Services;
using WheelOfFortune.Shared;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;
using Crypt = BCrypt.Net.BCrypt;
namespace WheelOfFortune.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenService _jwtTokenService;

        public UserController(DatabaseContext context, IMapper mapper, IConfiguration configuration, JwtTokenService jwtTokenService)
        {
            _context = context;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
        }
        [HttpGet]


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {

            var newUser = _mapper.Map<UserEntity>(registerDto);
            if (newUser.Role == Shared.Enums.Role.Admin)
            {
                return Conflict("Cannot register admin");
            }
            if (_context.Users.SingleOrDefault(u => u.Email.ToLower() == newUser.Email.ToLower()) != null)
            {
                return Conflict("User with this email is exist");
            }

            try
            {
                newUser.Password = Crypt.HashPassword(newUser.Password);
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(_mapper.Map<ReadUserDto>(newUser));
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDto authenticateDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == authenticateDto.Email);
            if (user == null)
            {
                return NotFound();
            }
            if (!Crypt.Verify(authenticateDto.Password, user.Password))
            {
                return Conflict("Incorrect password");
            }

            var refreshTokenLifetime = int.Parse(_configuration["JwtAuth:RefreshTokenLifetime"]);

            var refreshTokenEntity = new RefreshTokenEntity()
            {
                UserId = user.Id,
                ExpirationTime = DateTime.UtcNow.AddDays(refreshTokenLifetime)
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            var tokenPair = _jwtTokenService.IssueTokenPair(user, refreshTokenEntity.Id);

            return Ok(tokenPair);
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var refreshTokenClaims = _jwtTokenService.ParseToken(refreshToken);
            if (refreshTokenClaims is null)
            {
                return BadRequest("Invalid refresh token was provided.");
            }

            var refreshTokenId = int.Parse(refreshTokenClaims["Jti"]);
            var refreshTokenEntity = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Id == refreshTokenId);
            if (refreshTokenEntity is null)
            {
                return Conflict("Provided refresh token has already been used.");
            }

            _context.RefreshTokens.Remove(refreshTokenEntity);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Id == int.Parse(refreshTokenClaims["Sub"]));
            
            if (user is null)
            {
                return BadRequest("User don't exist");
            }

            var refreshTokenLifetime = int.Parse(_configuration["JwtAuth:RefreshTokenLifetime"]);
            var newRefreshTokenEntity = new RefreshTokenEntity
            {
                UserId = user.Id,
                ExpirationTime = DateTime.UtcNow.AddDays(refreshTokenLifetime)
            };
            _context.RefreshTokens.Add(newRefreshTokenEntity);
            await _context.SaveChangesAsync();

            var tokenPair = _jwtTokenService.IssueTokenPair(user, refreshTokenEntity.Id);
            
            return Ok(tokenPair);
        }

    }
}