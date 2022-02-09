using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFF.API.Domain.Authorization;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Repositories;
using SFF.API.Services.Interfaces;
using SFF.API.Transfer;

namespace SFF.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private IFilmService _filmService;

        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllFilms()
        {
            var user = (User)HttpContext.Items["User"];

            try
            {

                if (user == null)
                {
                    var filmsNoCopies = _filmService.FilmNoCopiesList().ToList();
                    return Ok(filmsNoCopies);
                }
                if (user != null)
                {
                   var filmsIncludeCopies = _filmService.FilmListIncludeCopies().ToList();

                   return Ok(filmsIncludeCopies); 
                }
               
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return BadRequest();
        }
        
        [HttpPut]
        public async Task<ActionResult<Film>> AddMovie(CreateFilmRequestData model)
        {
            var user = (User)HttpContext.Items["User"];
            if (ModelState.IsValid && user.Role == "admin")
            {
                try
                {
                var result = _filmService.CreateNewFilm(model);
                return await result;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
             return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet("{filmId}")]
        public IActionResult GetFilms(string filmId)
        {
            try
            {
                var filmsNoCopy = _filmService.FilmNoCopiesList();
                bool filmExist = filmsNoCopy.Any(f => f.FilmId == filmId);
                if (!filmExist) throw new Exception("Det finns ingen film med detta id");

                var user = (User)HttpContext.Items["User"];

                if (user == null || user.Role == "filmstudio" || user.Role == "admin")
                {
                    if (user == null)
                    {
                        filmsNoCopy = filmsNoCopy
                        .Where(f => f.FilmId == filmId);
                        
                        return Ok(filmsNoCopy);
                    }
                
                    if (user.Role == "filmstudio" || user.Role == "admin")
                    {
                        var filmsCopy = _filmService.FilmListIncludeCopies()
                        .Where(f => f.FilmId == filmId);

                        return Ok(filmsCopy);
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
