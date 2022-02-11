using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFF.API.Domain.Authorization;
using SFF.API.Domain.Entities;
using SFF.API.Services.Interfaces;
using SFF.API.Transfer;

namespace SFF.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;   
        }

        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponceData>> Register(UserRegisterRequestData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.IsAdmin)
                    {
                        User newUser = await _userService.Register(model);
                        return _mapper.Map<UserRegisterResponceData>(newUser);        
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("IsAdmin needs to be true");  
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<UserAuthenticateResponceData> Authenticate(UserAuthenticateRequestData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = _userService.Authenticate(model);
                    return response;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();       
        }
        
    }
}
