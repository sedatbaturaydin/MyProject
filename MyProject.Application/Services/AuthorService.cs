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
            var Authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(Authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(Guid id)
        {
            var Author = await _unitOfWork.Authors.GetByIdAsync(id);
            return _mapper.Map<AuthorDto>(Author);
        }

        public async Task<AuthorDto> AddAsync(AuthorDto AuthorDto)
        {
            var Author = _mapper.Map<Author>(AuthorDto);
            await _unitOfWork.Authors.AddAsync(Author);
            return _mapper.Map<AuthorDto>(Author);
        }

        public async Task UpdateAsync(AuthorDto AuthorDto)
        {
            var Author = _mapper.Map<Author>(AuthorDto);
            await _unitOfWork.Authors.UpdateAsync(Author);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Authors.DeleteAsync(id);
        }
    }
}
