using System;
using System.Collections.Generic;


namespace SFF.API.Domain.Entities.Interfaces
{
    public interface IFilmCopy
    {
        public string FilmCopyId { get; set; }
        public string FilmId { get; set; }
        public bool RentedOut { get; set; }
        public string FilmStudioId { get; set; }
        public DateTime Rented { get; set; }
        
    }

}