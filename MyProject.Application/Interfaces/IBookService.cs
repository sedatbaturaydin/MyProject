using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyProject.Application.DTOs;
using MyProject.Core.Entities;
using MyProject.Core.Interfaces;

namespace MyProject.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<BookDto> AddAsync(BookDto bookDto);
        Task UpdateAsync(BookDto bookDto);
        Task DeleteAsync(Guid id);

        Task AddRangeAsync(List<BookDto> booksDto);
    }
}
