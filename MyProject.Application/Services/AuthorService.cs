using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Core.Entities;
using MyProject.Core.Interfaces;

namespace MyProject.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(Guid id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> AddAsync(AuthorDto AuthorDto)
        {
            var author = _mapper.Map<Author>(AuthorDto);
            await _unitOfWork.Authors.AddAsync(author);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAsync(AuthorDto AuthorDto)
        {
            var author = _mapper.Map<Author>(AuthorDto);
            await _unitOfWork.Authors.UpdateAsync(author);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Authors.DeleteAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetAuthorSelectListAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return authors?.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList() ?? new List<SelectListItem>(); // NULL GELİRSE BOŞ DÖN
        }

        public async Task AddRangeAsync(List<AuthorDto> authorsDto)
        {
            var authors = _mapper.Map<List<Author>>(authorsDto);
            await _unitOfWork.Authors.AddRangeAsync(authors);
        }
    }
}
