using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFF.API.Domain.Entities.FilmEntity;
using SFF.API.Domain.Services;

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
        public async Task<IActionResult> GetAllFilmsAcync()
        {
 
            //var filmsIncludeCopies = await _filmService.FilmListIncludeCopiesAsync();
            var filmsNoCopies = await _filmService.FilmNoCopiesListAsync();

            try
            {
                //var user = await userManager.GetUserAsync(User);
     
                //if(user == "admin" || user == "filmstudio"){
                //return Ok(filmsIncludeCopies);
                return Ok(filmsNoCopies);
               
                //else return Ok(filmsNoCopies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }
        
    }
}
