using System;
using System.Collections.Generic;


namespace SFF.API.Domain.Services
{
    public interface IRegisterFilmStudioService
    {
        public string FilmStudioName { get; set; }
        public string FilmStudioCity { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }

}