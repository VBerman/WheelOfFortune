using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Shared.Model;
using WheelOfFortune.Shared.Model.Record;

namespace WheelOfFortune.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RecordController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RecordController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([FromBody] CreateRecordDto createRecordDto)
        {
            var newRecord = _mapper.Map<RecordEntity>(createRecordDto);
            await _context.Records.AddAsync(newRecord);
            await _context.SaveChangesAsync();
            return Ok(newRecord.Id);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRecordsByUser()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var result = await _context.Records.Where(r => (r.ClientId == userId | r.RealEstate.LandlordId == userId) & r.DateTime > DateTime.Now)
                .Select(r => new ReadRecordDto()
                {
                    Id = r.Id,
                    ClientFullName = r.Client.FullName,
                    Address = r.RealEstate.Address,
                    ClientId = r.ClientId,
                    DateTime = r.DateTime,
                    LandlordFullName = r.RealEstate.Landlord.FullName,
                    RealEstateId = r.RealEstateId,
                    LandlordId = r.RealEstate.LandlordId
                }).ToListAsync();
            return Ok(result);
        }

        [HttpDelete("{recordId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int recordId)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Sub").Value);
            var record = await _context.Records.Include(r => r.RealEstate).FirstOrDefaultAsync(r => r.Id == recordId);
            if (record is null)
            {
                return NotFound();
            }
            if (record.ClientId == userId | record.RealEstate.LandlordId == userId)
            {
                _context.Records.Remove(record);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Forbid();
        }

    }
}
