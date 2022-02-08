using System;
using System.Collections.Generic;
using SFF.API.Domain.Entities;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class UserAuthenticateResponceData
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudios { get; set; }
        public string Token { get; set; }
    }
}