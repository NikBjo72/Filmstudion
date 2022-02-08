using AutoMapper;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;

namespace SFF.API.Mapping
{
    public class FilmMapping : Profile
    {
        public FilmMapping()
        {
            this.CreateMap<Film, CreateFilmRequestData>()
            .ReverseMap();
        }
    }    
}    