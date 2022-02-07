using System;
using System.Collections.Generic;
using SFF.API.Models.Interfaces;

namespace SFF.API.Models
{
    public class UserAuthenticateModelRequest : IUserAuthenticate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}