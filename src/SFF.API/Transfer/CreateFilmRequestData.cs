using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class CreateFilmRequestData : ICreateFilm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }
        [Required]
        public bool AvailableForRent { get; set; }
        [Required]
        public int MaxRentDays { get; set; }
    }
}