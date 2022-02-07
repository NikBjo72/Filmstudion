using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SFF.API.Domain.Entities.Interfaces;

namespace SFF.API.Domain.Entities
{
    public class User : IUser
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudios { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}