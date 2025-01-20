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
    public class ReservationsController : ControllerBase
    {
        private readonly BookingDbContext _context;

        public ReservationsController(BookingDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // GET: api/Reservations/Stay/5
        [HttpGet("Stay/{stayId}")]
        public async Task<ActionResult<List<Stay>>> GetReservationsByStayId(int stayId)
        {
            var stay = await _context.Stays
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == stayId);

            if (stay == null || stay.Reservations == null || !stay.Reservations.Any())
            {
                return NotFound();
            }

            return Ok(stay.Reservations);
        }

        // GET: api/Reservations/Guest/5
        [HttpGet("Guest/{guestId}")]
        public async Task<ActionResult<List<Stay>>> GetStaysByPartnerId(int guestId)
        {
            var guest = await _context.Guests
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == guestId);

            if (guest == null || guest.Reservations == null || !guest.Reservations.Any())
            {
                return NotFound();
            }

            return Ok(guest.Reservations);
        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
