﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingApp.Models
{
    public class Partner
    {
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        // navigation properties
        [JsonIgnore]
        public ICollection<Stay> Stays { get; } = new List<Stay>();
    }
}
