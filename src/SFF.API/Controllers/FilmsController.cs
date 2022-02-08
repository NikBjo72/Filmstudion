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
        //[AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllFilms()
        {
            var filmsIncludeCopies = _filmService.FilmListIncludeCopies();
            //var filmsNoCopies = _filmService.FilmNoCopiesList().ToList();

            try
            {
                //var user = await userManager.GetUserAsync(User);
     
                //if(user == "admin" || user == "filmstudio"){
                //return Ok(filmsIncludeCopies);
                return Ok(filmsIncludeCopies);
               
                //else return Ok(filmsNoCopies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }
        //[AllowAnonymous]
        [HttpPut]
        public async Task<ActionResult<Film>> AddMovie(CreateFilmRequestData model)
        {
            if (ModelState.IsValid)
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
             return BadRequest();
        }
        
    }
}
