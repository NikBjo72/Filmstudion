using System;
using System.Collections.Generic;


namespace SFF.API.Transfer.Interfaces
{
    public interface IUserAuthenticate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}