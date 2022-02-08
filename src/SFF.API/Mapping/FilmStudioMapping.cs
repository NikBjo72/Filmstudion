using AutoMapper;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;

namespace SFF.API.Mapping
{
    public class FilmStudioMapping : Profile
    {
        public FilmStudioMapping()
        {

            this.CreateMap<FilmStudio, RegisterFilmStudioRequestData>()
            .ReverseMap();

            this.CreateMap<FilmStudio, RegisterFilmStudioResponceData>()
            .ReverseMap();

        }
    }    
}    