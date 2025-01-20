using BookingApp.Models.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Models
{
    public class ReservationDTO
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public int StayId { get; set; }
        public int GuestId { get; set; }
    }
}
