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
            return BadRequest();
           
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllFilmsStudios()
        {
            try
            {
                var user = (User)HttpContext.Items["User"];

                if (user == null || user.Role == "filmstudio")
                {
                    var filmstudiosNoCity = _filmStudioService.ListFilmStudioIncludeFilmCopiesNoCity();
                    return Ok(filmstudiosNoCity);
                }

                if (user.Role == "admin")
                {
                    var filmstudios = _filmStudioService.QueryableFilmStudioIncludeFilmCopies();
                    return Ok(filmstudios);
                }
            }
            catch (Exception)
            {
               return BadRequest(); 
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{studioId}")]
        public IActionResult GetFilmsStudio(string studioId)
        {
            try
            {
                var filmstudio = _filmStudioService.QueryableFilmStudioIncludeFilmCopies();
                bool filmStudioExist = filmstudio.Any(s => s.FilmStudioId == studioId);
                if (!filmStudioExist) throw new Exception("Det finns ingen filmstudio med detta id");

                var user = (User)HttpContext.Items["User"];

                if (user == null || user.Role == "filmstudio" || user.Role == "admin")
                {
                    if (user == null)
                    {
                        var filmstudioNoCity = _filmStudioService
                        .ListFilmStudioIncludeFilmCopiesNoCity()
                        .Where(s => s.FilmStudioId == studioId);
                        
                        return Ok(filmstudioNoCity);
                    }
                
                    if (user.FilmStudioId == studioId || user.Role == "admin")
                    {
                        filmstudio = filmstudio
                        .Where(s => s.FilmStudioId == studioId);

                        return Ok(filmstudio);
                    }
                }       
            }
            catch (Exception ex)
            {
               return NotFound(ex.Message); 
            }
            return BadRequest();
        }
    }
}
