using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SFF.API.Domain.Entities;
using SFF.API.Domain.Entities.Interfaces;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class PatchFilmRequestData : Ifilm
    {
        [JsonIgnore]
        public string FilmId { get; set; }
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
        [JsonIgnore]
        public List<FilmCopy> FilmCopies { get; set; }
    }
}