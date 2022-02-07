using System;
using System.Collections.Generic;
using SFF.API.Domain.Entities;
using SFF.API.Models.Interfaces;

namespace SFF.API.Models
{
    public class UserAuthenticateModelResponce
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudios { get; set; }
        public string Token { get; set; }
    }
}