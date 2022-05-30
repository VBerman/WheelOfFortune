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
        public async Task<IActionResult> AddRent(CreateRentDto createRentDto)
        {
            var newRent = _mapper.Map<RentEntity>(createRentDto);
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
    }
}
