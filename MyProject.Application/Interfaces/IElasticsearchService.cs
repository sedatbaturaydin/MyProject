using System.Collections.Generic;
using System.Threading.Tasks;
using MyProject.Application.DTOs;

namespace MyProject.Application.Interfaces
{
    public interface IElasticsearchService
    {
        Task<bool> IndexExistsAsync();
        Task CreateIndexAsync();
        Task IndexDocumentAsync(BookDto book);
        Task EnsureIndexExistsAsync();
        Task<List<BookDto>> SearchBooksAsync(string query);
    }
}
