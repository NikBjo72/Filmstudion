﻿using System;
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
    public class FilmStudiosController : ControllerBase
    {
        private readonly IFilmStudioService _filmStudioService;
        private readonly IMapper _mapper;

        public FilmStudiosController(IFilmStudioService filmStudioService, IMapper mapper)
        {
            _filmStudioService = filmStudioService;
            _mapper = mapper;   
        }

        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterFilmStudioResponceData>> Register(RegisterFilmStudioRequestData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                        RegisterFilmStudioResponceData newFilmStudio = await _filmStudioService.RegisterFilmStudio(model);
                        return _mapper.Map<RegisterFilmStudioResponceData>(newFilmStudio);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("IsAdmin needs to be true");
           
        }
        
    }
}