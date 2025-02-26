using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Application.DTOs;

namespace MyProject.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(Guid id);
        Task<AuthorDto> AddAsync(AuthorDto authorDto);
        Task UpdateAsync(AuthorDto authorDto);
        Task DeleteAsync(Guid id);
    }
}
