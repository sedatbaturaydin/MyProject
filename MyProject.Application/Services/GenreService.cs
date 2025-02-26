using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Core.Entities;
using MyProject.Core.Interfaces;

namespace MyProject.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var Genres = await _unitOfWork.Genres.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(Genres);
        }

        public async Task<GenreDto> GetGenreByIdAsync(Guid id)
        {
            var Genre = await _unitOfWork.Genres.GetByIdAsync(id);
            return _mapper.Map<GenreDto>(Genre);
        }

        public async Task<GenreDto> AddAsync(GenreDto GenreDto)
        {
            var Genre = _mapper.Map<Genre>(GenreDto);
            await _unitOfWork.Genres.AddAsync(Genre);
            return _mapper.Map<GenreDto>(Genre);
        }

        public async Task UpdateAsync(GenreDto GenreDto)
        {
            var Genre = _mapper.Map<Genre>(GenreDto);
            await _unitOfWork.Genres.UpdateAsync(Genre);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Genres.DeleteAsync(id);
        }
    }
}
