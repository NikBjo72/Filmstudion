using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using SFF.API.Domain.Entities.Interfaces;

namespace SFF.API.Domain.Entities
{
    public class FilmStudio : IFilmStudio
    {
        public string FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }

    }

}