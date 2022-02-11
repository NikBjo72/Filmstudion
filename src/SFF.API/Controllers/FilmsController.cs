using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFF.API.Domain.Authorization;
using SFF.API.Domain.Entities;
using SFF.API.Domain.Entities.Interfaces;
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
        private IFilmStudioService _filmStudioService;
        private IFilmCopyService _filmCopyService;

        public FilmsController(IFilmService filmService, IFilmStudioService filmStudioService, IFilmCopyService filmCopyService)
        {
            _filmService = filmService;
            _filmStudioService = filmStudioService;
            _filmCopyService = filmCopyService;
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
        public async Task<ActionResult<IFilm>> AddMovie(CreateFilmRequestData model)
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

        [HttpPatch("{filmId}")]
        public ActionResult<IFilm> EditMovie(string filmId, PatchFilmRequestData model)
        {
            var user = (User)HttpContext.Items["User"];
            if (user.Role != "admin")
            {
                return Unauthorized("Du har inte behörighet för denna åtgärden");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    return _filmService.UpdateFilm(filmId, model);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("rent")]
        public IActionResult RentFilm(string filmId, string studioId)
        {
            
            // Kontrollerar om idn är rätt samt om det är en admin.
            bool filmExist = _filmService
                .FilmNoCopiesList()
                .Any(f => f.FilmId == filmId);
            if (!filmExist) return Conflict("Det finns ingen film med detta id");

            bool studioExist = _filmStudioService
                .QueryableFilmStudioNoFilmCopies()
                .Any(f => f.FilmStudioId == studioId);
            if (!studioExist) return Conflict("Det finns ingen filmstudio med detta id");

            bool filmCopiesToRent = _filmCopyService
                .FilmCopyList()
                .Where(f => f.FilmId == filmId)
                .Any(f => f.RentedOut == false);
            if (!filmCopiesToRent) return Conflict("Det finns inga lediga kopior av denna filmen att hyra");

            var user = (User)HttpContext.Items["User"];
            if (user.Role == "admin") return Unauthorized("Som admin kan du inte hyra filmer");

            try
            {
                _filmService.RentFilm(filmId, studioId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
        }

        [HttpPost("return")]
        public IActionResult ReturnFilm(string filmId, string studioId)
        {
            
            // Kontrollerar om idn är rätt samt om det är en admin.
            bool filmExist = _filmService
                .FilmNoCopiesList()
                .Any(f => f.FilmId == filmId);
            if (!filmExist) return Conflict("Det finns ingen film med detta id");

            bool studioExist = _filmStudioService
                .QueryableFilmStudioNoFilmCopies()
                .Any(f => f.FilmStudioId == studioId);
            if (!studioExist) return Conflict("Det finns ingen filmstudio med detta id");

            bool filmCopyRented = _filmCopyService
                .FilmCopyList()
                .Where(f => f.FilmStudioId == studioId)
                .Any(f => f.FilmId == filmId);
            if (!filmCopyRented) return Conflict("Du har inte hyrt denna filmen");

            var user = (User)HttpContext.Items["User"];
            if (user.Role == "admin") return Unauthorized("Som admin kan du inte hyra filmer");

            try
            {
                _filmService.ReturnFilm(filmId, studioId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
    }
}
