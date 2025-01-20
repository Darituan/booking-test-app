using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models.Validation
{
    public class CorrectDateOrderAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var reservation = (Reservation)validationContext.ObjectInstance;

            if (reservation.StartDate >= reservation.EndDate)
            {
                return new ValidationResult("The reservation end date has to be after the start date.");
            }
            return ValidationResult.Success;
        }
    }
}
