using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BookingApp.Models.Validation;

namespace BookingApp.Models
{
    public class Stay
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Address { get; set; }

        // defines whether the property is a house or an apartment
        public bool IsHouse { get; set; }

        // defines whether every room and amenity in the property is accessible to guests
        public bool FullPropertyAccess { get; set; }

        [GreaterThanZero]
        public int NumberOfRooms { get; set; }

        // defines maximum number of people who can book a stay
        [GreaterThanZero]
        public int MaxPeople { get; set; }

        public bool PetsAllowed { get; set; }

        [GreaterThanZero]
        public int PricePerDay { get; set; }

        // navigation properties
        public ICollection<Reservation> Reservations { get; } = new List<Reservation>();
        [ForeignKey(nameof(Partner))]
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }

    }
}
