using System;
using System.Collections.Generic;
using SFF.API.Domain.Entities;

namespace SFF.API.Domain.Entities.Interfaces
{
    public interface IFilmStudio
    {
        public string FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }
    }

}