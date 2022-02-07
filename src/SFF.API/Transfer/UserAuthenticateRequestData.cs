using System;
using System.Collections.Generic;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class UserAuthenticateRequestData : IUserAuthenticate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}