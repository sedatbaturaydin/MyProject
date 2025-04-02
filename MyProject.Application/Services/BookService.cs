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
    public class BookService: IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> AddAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.UpdateAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Books.DeleteAsync(id);
        }

        public async Task AddRangeAsync(List<BookDto> booksDto)
        {
            var books = _mapper.Map<List<Book>>(booksDto);
            await _unitOfWork.Books.AddRangeAsync(books);   
        }
    }
}
