using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFF.API.Domain.Entities;
using SFF.API.Services.Interfaces;
using SFF.API.Transfer;

namespace SFF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserControllers : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserControllers(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;   
        }

        
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequestData model)
        {

            try
            {
                _userService.Register(model);
                return Ok(_mapper.Map<UserRegisterResponceData>(model));
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }
        
    }
}
