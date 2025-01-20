using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaysController : ControllerBase
    {
        private readonly BookingDbContext _context;

        public StaysController(BookingDbContext context)
        {
            _context = context;
        }

        // GET: api/Stays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stay>>> GetStays()
        {
            return await _context.Stays.ToListAsync();
        }

        // GET: api/Stays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stay>> GetStay(int id)
        {
            var stay = await _context.Stays.FindAsync(id);

            if (stay == null)
            {
                return NotFound();
            }

            return stay;
        }

        // GET: api/Stays/country?country=Ukraine
        [HttpGet("country")]
        public async Task<ActionResult<List<Stay>>> GetStaysByCountry([FromQuery] string country)
        {
            var stays = await _context.Stays
                .Where(s => s.Country == country)
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }

        // GET: api/Stays/city?city=Kyiv
        [HttpGet("city")]
        public async Task<ActionResult<List<Stay>>> GetStaysByCity([FromQuery] string city)
        {
            var stays = await _context.Stays
                .Where(s => s.City == city)
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }
        
        // GET: api/Stays/pets?petsAllowed=True
        [HttpGet("pets")]
        public async Task<ActionResult<List<Stay>>> GetStaysByPetsAllowed([FromQuery] bool petsAllowed)
        {
            var stays = await _context.Stays
                .Where(s => s.PetsAllowed == petsAllowed)
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }

        // GET: api/Stays/house?isHouse=True
        [HttpGet("house")]
        public async Task<ActionResult<List<Stay>>> GetStaysByIsHouse([FromQuery] bool isHouse)
        {
            var stays = await _context.Stays
                .Where(s => s.IsHouse == isHouse)
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }

        // GET: api/Stays/name?name=Place
        [HttpGet("name")]
        public async Task<ActionResult<List<Stay>>> GetStaysByName([FromQuery] string name) 
        {
            var stays = await _context.Stays
                .Where(s => s.Name.Contains(name))
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }

        // GET: api/Stays/price?price=100
        [HttpGet("price")]
        public async Task<ActionResult<List<Stay>>> GetStaysByMaxPrice([FromQuery] int price)
        {
            var stays = await _context.Stays
                .Where(s => s.PricePerDay <= price)
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }

        // GET: api/Stays/guests?numGuests=2
        [HttpGet("guests")]
        public async Task<ActionResult<List<Stay>>> GetStaysByNumberOfGuests([FromQuery] int numGuests)
        {
            var stays = await _context.Stays
                .Where(s => s.MaxPeople >= numGuests)
                .ToListAsync();

            if (stays == null || !stays.Any())
            {
                return NotFound();
            }

            return stays;
        }


        // GET: api/Stays/Partner/5
        [HttpGet("Partner/{partnerId}")]
        public async Task<ActionResult<List<Stay>>> GetStaysByPartnerId(int partnerId)
        {
            var partner = await _context.Partners
                .Include(p => p.Stays)
                .FirstOrDefaultAsync(p => p.Id == partnerId);

            if (partner == null || partner.Stays == null || !partner.Stays.Any())
            {
                return NotFound();
            }

            return Ok(partner.Stays);
        }



        // PUT: api/Stays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStay(int id, Stay stay)
        {
            if (id != stay.Id)
            {
                return BadRequest();
            }

            _context.Entry(stay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stay>> PostStay([Bind("Name,Country,Description,City,Address,IsHouse,FullPropertyAccess,NumberOfRooms,MaxPeople,PetsAllowed,StartDate,EndDate,PricePerDay,PartnerId")] StayDto stayDto)
        {
            //_context.Stays.Add(stay);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetStay", new { id = stay.Id }, stay);

            var partner = await _context.Partners.FindAsync(stayDto.PartnerId);

            if (partner == null) { return NotFound("Partner not found."); }

            var stay = new Stay
            {
                Name = stayDto.Name,
                Country = stayDto.Country,
                Description = stayDto.Description,
                City = stayDto.City,
                Address = stayDto.Address,
                IsHouse = stayDto.IsHouse,
                FullPropertyAccess = stayDto.FullPropertyAccess,
                NumberOfRooms = stayDto.NumberOfRooms,
                MaxPeople = stayDto.MaxPeople,
                PetsAllowed = stayDto.PetsAllowed,
                PricePerDay = stayDto.PricePerDay,
                PartnerId = stayDto.PartnerId,
                Partner = partner
            };

            _context.Stays.Add(stay);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStay), new { id = stay.Id }, stay);
        }

        // DELETE: api/Stays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStay(int id)
        {
            var stay = await _context.Stays.FindAsync(id);
            if (stay == null)
            {
                return NotFound();
            }

            _context.Stays.Remove(stay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StayExists(int id)
        {
            return _context.Stays.Any(e => e.Id == id);
        }
    }
}
