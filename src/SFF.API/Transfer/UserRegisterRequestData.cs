using System;
using System.Collections.Generic;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class UserRegisterRequestData : IRegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}