using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using SFF.API.Domain.Entities.Interfaces;

namespace SFF.API.Domain.Entities
{
    public class User : IdentityUser, IUser
    {
        //public string Id { get; set; }
        public string Role { get; set; }
        //public string UserName { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudios { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}