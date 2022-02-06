using System;
using System.Collections.Generic;
using SFF.API.Domain.Entities.FilmEntity;
using SFF.API.Domain.Entities.UserEntity;

namespace SFF.API.Domain.Entities.FilmStudioEntity
{
    public class FilmStudio : IFilmStudio, IUser
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