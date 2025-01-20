using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.Validation
{
    public class NoMorePeopleThanMaxAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;

            if (reservation.NumberOfPeople > reservation.Stay.MaxPeople)
            {
                return new ValidationResult("The number of guests cannot exceed the maximum for chosen property");
            }
            return ValidationResult.Success;
        }
    }
}
