using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WheelOfFortune.Shared;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> HelloWorld()
        {
            return Ok("hello");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var newUser = _mapper.Map<UserEntity>(registerDto); 
            return Ok();
        }



    }
}
