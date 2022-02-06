using System;
using System.Collections.Generic;


namespace SFF.API.Domain.Entities.FilmEntity
{
    public class Film : Ifilm
    {
        public string FilmId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public int NumberOfCopies { get; set; }
        public bool AvailableForRent { get; set; }
        public int MaxRentDays { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }
    }

}