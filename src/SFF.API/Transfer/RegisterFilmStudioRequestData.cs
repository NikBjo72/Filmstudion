using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class RegisterFilmStudioRequestData : IRegisterFilmStudio
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}