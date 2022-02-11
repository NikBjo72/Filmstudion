using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SFF.API.Domain.Entities;

namespace SFF.API.Domain.Entities.Interfaces
{
    public interface IUser
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudios { get; set; }
        public string Password { get; set; }
        //public string Token { get; set; }
    }

}