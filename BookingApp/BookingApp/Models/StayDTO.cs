using BookingApp.Models.Validation;

namespace BookingApp.Models
{
    public class StayDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public bool IsHouse { get; set; }
        public bool FullPropertyAccess { get; set; }
        public int NumberOfRooms { get; set; }
        public int MaxPeople { get; set; }
        public bool PetsAllowed { get; set; }
        public int PricePerDay { get; set; }
        public int PartnerId { get; set; }
    }

}
