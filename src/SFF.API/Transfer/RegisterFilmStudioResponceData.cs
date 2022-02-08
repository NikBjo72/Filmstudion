using System;
using System.Collections.Generic;
using SFF.API.Transfer.Interfaces;
using SFF.API.Domain.Entities.Interfaces;
using SFF.API.Domain.Entities;

namespace SFF.API.Transfer
{
    public class RegisterFilmStudioResponceData : IFilmStudio
    {
        public string FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }
    }
}