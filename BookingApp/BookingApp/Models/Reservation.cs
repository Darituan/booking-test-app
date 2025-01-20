using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models.Validation;

namespace BookingApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [NoOverlappingReservations]
        public DateOnly StartDate { get; set; }

        [CorrectDateOrder]
        public DateOnly EndDate { get; set; }

        [GreaterThanZero]
        [NoMorePeopleThanMax]
        public int NumberOfPeople { get; set; }

        public int Price { get; set;}

        // navigation properties
        [ForeignKey(nameof(Stay))]
        public int StayId { get; set; }
        [ForeignKey(nameof(Guest))]
        public int GuestId { get; set; }
    }
}
