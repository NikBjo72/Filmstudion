using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFF.API.Domain.Entities;
using SFF.API.Services.Interfaces;

namespace SFF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;   
        }

        
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
        
    }
}
