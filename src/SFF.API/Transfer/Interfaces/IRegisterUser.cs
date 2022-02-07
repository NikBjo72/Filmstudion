using System;
using System.Collections.Generic;


namespace SFF.API.Transfer.Interfaces
{
    public interface IRegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }

}