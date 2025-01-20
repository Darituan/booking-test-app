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

        public int Price 
        { 
            get 
            {
                int numberOfDays = (EndDate.ToDateTime(TimeOnly.MinValue) - StartDate.ToDateTime(TimeOnly.MinValue)).Days;
                return Stay.PricePerDay * NumberOfPeople * numberOfDays;
            } 
        }

        // navigation properties
        [ForeignKey(nameof(Stay))]
        public int StayId { get; set; }
        public Stay Stay { get; set; }
        [ForeignKey(nameof(Guest))]
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
