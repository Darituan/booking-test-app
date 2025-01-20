using System.ComponentModel.DataAnnotations;
using BookingApp.Models;

namespace BookingApp.Models.Validation
{
    public class NoOverlappingReservationsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;
            var context = (BookingDbContext)validationContext.GetService(typeof(BookingDbContext));

            var overlappingReservations = context.Reservations
                .Where(r => r.StayId == reservation.StayId &&
                            r.Id != reservation.Id &&
                            (reservation.StartDate <= r.EndDate && reservation.EndDate >= r.StartDate))
                .ToList();

            if (overlappingReservations.Any())
            {
                return new ValidationResult("The reservation dates overlap with an existing reservation.");
            }
            return ValidationResult.Success;
        }
    }
}
