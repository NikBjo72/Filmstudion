using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Persistence.Repositories;
using SFF.API.Services.Interfaces;
using SFF.API.Transfer;

namespace SFF.API.Services
{
    public class FilmCopyService : IFilmCopyService
    {
        private readonly IMapper _mapper;
        private readonly IFilmCopyRepository _filmCopyRepository;
        public FilmCopyService(IMapper mapper, IFilmCopyRepository filmCopyRepository)
        {
            _mapper = mapper;
            _filmCopyRepository = filmCopyRepository;
        }
        public IQueryable<FilmCopy> FilmCopyList()
        {
            return _filmCopyRepository.FilmCopyList();
        }
        public IQueryable<FilmCopy> GetFilmCopiesByFilmId(string filmId)
        {
            var filmCopies = _filmCopyRepository.GetFilmCopiesByFilmId(filmId);
            return filmCopies;
        }
        public void AddAsync(FilmCopy filmCopy)
        {
            _filmCopyRepository.AddAsync(filmCopy);
        }

    }

}