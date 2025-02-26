﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Application.DTOs;

namespace MyProject.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(Guid id);
        Task<GenreDto> AddAsync(GenreDto genreDto);
        Task UpdateAsync(GenreDto genreDto);
        Task DeleteAsync(Guid id);
    }
}
