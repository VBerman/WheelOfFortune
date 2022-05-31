using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.RealEstate;

namespace WheelOfFortune.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RealEstateController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RealEstateController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Create([FromBody] CreateRealEstateDto createRealEstateDto)
        {
            var newRealEstate = _mapper.Map<RealEstateEntity>(createRealEstateDto);
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            newRealEstate.LandlordId = userId;
            newRealEstate.RejectionReason = "Еще не проверена";

            await _context.RealEstates.AddAsync(newRealEstate);
            await _context.SaveChangesAsync();
            return Ok(newRealEstate.Id);
        }
        [HttpPost]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Update([FromBody] UpdateRealEstateDto updateRealEstateDto)
        {
            var updatedRealEstate = _context.RealEstates.FirstOrDefault(r => r.Id == updateRealEstateDto.Id);
            if (updatedRealEstate is null)
            {
                return Conflict("Not found real estate");
            }
            if (updatedRealEstate.LandlordId != int.Parse(User.Claims.First(c => c.Type == "Sub").Value))
            {
                return Conflict("Haven't permissions for update");
            }
            _mapper.Map(updateRealEstateDto, updatedRealEstate);
            updatedRealEstate.RejectionReason = "Еще не проверена";
            updatedRealEstate.IsConfirmed = false;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateConfirmed([FromBody] UpdateConfirmedRealEstateDto updateConfirmedRealEstateDto)
        {
            var updatedRealEstate = _context.RealEstates.FirstOrDefault(r => r.Id == updateConfirmedRealEstateDto.Id);
            if (updatedRealEstate is null)
            {
                return Conflict("Not found real estate");
            }
            _mapper.Map(updateConfirmedRealEstateDto, updatedRealEstate);
            updatedRealEstate.AdminConfirmedId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpGet]
        public async Task<IActionResult> Get(bool? isConfirmed)
        {

            if (isConfirmed is null)
            {
                var result = await _context.RealEstates.Select(r => _mapper.Map<ReadRealEstateDto>(r)).ToListAsync();
                return Ok(result);
            }
            else
            {
                var result = await _context.RealEstates.Where(r => r.IsConfirmed == isConfirmed).Select(r => _mapper.Map<ReadRealEstateDto>(r)).ToListAsync();
                return Ok(result);

            }
        }

        [HttpGet("{realEstateId:int}")]
        public async Task<IActionResult> Get(int realEstateId)
        {

            var result = await _context.RealEstates.Include(r => r.Landlord).FirstOrDefaultAsync(r => r.Id == realEstateId);
            if (result is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<ReadRealEstateDto>(result);
            response.LandLordFullName = result.Landlord.FullName;
            return Ok(response);
        }
        [HttpPost("{realEstateId:int}")]
        [Authorize(Roles = "Admin,Landlord")]
        public async Task<IActionResult> Delete(int realEstateId)
        {
            var realEstate = await _context.RealEstates.FirstOrDefaultAsync(r => r.Id == realEstateId);
            if (realEstate is null)
            {
                return NotFound();
            }
            if (User.IsInRole("Landlord") & int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Sub")?.Value) != realEstate.LandlordId)
            {
                return Forbid();
            }
            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
