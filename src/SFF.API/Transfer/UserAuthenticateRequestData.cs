using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class UserAuthenticateRequestData : IUserAuthenticate
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}