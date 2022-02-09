using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class MyStudioController : ControllerBase
    {
        private IFilmService _filmService;
        private IFilmStudioService _filmStudioService;
        private IFilmCopyService _filmCopyService;

        public MyStudioController(IFilmService filmService, IFilmStudioService filmStudioService, IFilmCopyService filmCopyService)
        {
            _filmService = filmService;
            _filmStudioService = filmStudioService;
            _filmCopyService = filmCopyService;
        }

        [HttpGet("rentals")]
        public ActionResult GetAllRentedFilms()
        {
            var user = (User)HttpContext.Items["User"];
            string userId = user.FilmStudioId;

            if(user.Role == "admin") return BadRequest("Du är autensierad som admin");

            try
            {
                var result = _filmService.GetAllRentedFilms(userId);
               return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
