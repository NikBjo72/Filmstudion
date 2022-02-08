using System;
using System.Collections.Generic;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class UserRegisterResponceData
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}