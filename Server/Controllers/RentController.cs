using AutoMapper;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.Rent;

namespace WheelOfFortune.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RentController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Add(CreateRentDto createRentDto)
        {
            var client = _context.Users.FirstOrDefault(u => u.Email.ToLower() == createRentDto.ClientEmail.ToLower());
            if (client is null)
            {
                return NotFound();
            }
            var newRent = _mapper.Map<RentEntity>(createRentDto);
            newRent.ClientId = client.Id;
            var realEstate = await _context.RealEstates.Include(r => r.Rents).FirstOrDefaultAsync(r => r.Id == newRent.RealEstateId);
            if (realEstate is null)
            {
                return NotFound("Real estate don't found");
            }
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            if (realEstate.LandlordId != userId)
            {
                return Forbid();
            }
            if (realEstate.Rents.FirstOrDefault(r => new TimeRange(r.StartRentDate, r.EndRentDate).OverlapsWith(new TimeRange(newRent.StartRentDate, newRent.EndRentDate))) != null)
            {
                return BadRequest("On this date rent already exist");
            }
            try
            {

                await _context.Rents.AddAsync(newRent);
                await _context.SaveChangesAsync();
                return Ok(newRent.Id);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{realEstateId:int}")]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> GetByRealEstate(int realEstateId)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var realEstate = await _context.RealEstates.FirstOrDefaultAsync(r => r.LandlordId == userId & r.Id == realEstateId);
            if (realEstate is null)
            {
                return Forbid();
            }
            try
            {
                var rents = await _context.Rents.Include(r => r.RealEstate).Include(r => r.Client).Where(r => r.RealEstateId == realEstateId).ToListAsync();

                return Ok(_mapper.Map<List<ReadRentDto>>(rents));

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetByClient()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var rents = await _context.Rents.Include(r => r.RealEstate).Include(r => r.Client).Where(r => r.ClientId == userId).ToListAsync();
            return Ok(_mapper.Map<List<ReadRentDto>>(rents));
        }
        [HttpGet]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> GetByLandlord()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var rents = await _context.Rents.Include(r => r.RealEstate).Include(r => r.Client).Where(r => r.RealEstate.LandlordId == userId).ToListAsync();
            return Ok(_mapper.Map<List<ReadRentDto>>(rents));
        }

        [HttpGet("{rendId:int}")]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> SetDebt(int rentId)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var rent = await _context.Rents.Include(r => r.RealEstate).FirstOrDefaultAsync(r => r.Id == rentId);
            if (rent is null)
            {
                return NotFound();
            }
            if (rent.RealEstate.LandlordId != userId)
            {
                return Forbid();
            }
            rent.IsDebt = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
