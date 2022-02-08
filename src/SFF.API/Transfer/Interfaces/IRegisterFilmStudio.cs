using System;
using System.Collections.Generic;


namespace SFF.API.Transfer.Interfaces
{
    public interface IRegisterFilmStudio
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}