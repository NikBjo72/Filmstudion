using System;
using System.Collections.Generic;


namespace SFF.API.Domain.Entities.FilmEntity
{
    public class FilmCopy : IFilmCopy
    {
        public string FilmCopyId { get; set; }
        public string FilmId { get; set; }
        public bool RentedOut { get; set; }
        public string FilmStudioId { get; set; }
        public DateTime Rented { get; set; }
        //public Film Film { get; set; }
    }

}