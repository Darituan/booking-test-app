using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.Validation
{
    public class NoMorePeopleThanMaxAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;
            var context = (BookingDbContext)validationContext.GetService(typeof(BookingDbContext));
            var stay = context.Stays.FirstOrDefault(s => s.Id == reservation.StayId);

            if (reservation.NumberOfPeople > stay.MaxPeople)
            {
                return new ValidationResult("The number of guests cannot exceed the maximum for chosen property");
            }
            return ValidationResult.Success;
        }
    }
}
