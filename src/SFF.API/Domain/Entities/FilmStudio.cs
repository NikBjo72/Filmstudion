using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SFF.API.Domain.Entities.Interfaces;

namespace SFF.API.Domain.Entities
{
    public class FilmStudio : IdentityUser, IFilmStudio, IUser
    {
        public string FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<FilmCopy> FilmCopies { get; set; }


        public string UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public FilmStudio FilmStudios { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

}