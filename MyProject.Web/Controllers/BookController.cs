using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Application.Extensions;
using MyProject.Application.Services;
using MyProject.Core.Entities;
using MyProject.Web.ViewModels;

namespace MyProject.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public BookController(IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BookCreateViewModel
            {
                Authors = await _authorService.GetAuthorSelectListAsync(),
                Genres = await _genreService.GetGenreSelectListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bookDto = new BookDto
                {
                    Id = Guid.NewGuid(),
                    Title = model.Book.Title,
                    PublishYear = model.Book.PublishYear,
                    Description = model.Book.Description,
                    Page = model.Book.Page,
                    AuthorId = model.Book.AuthorId,
                    GenreId = model.Book.GenreId
                };

                await _bookService.AddAsync(bookDto);
                return RedirectToAction("Index");
            }

            model.Authors = await _authorService.GetAuthorSelectListAsync();
            model.Genres = await _genreService.GetGenreSelectListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SeedData()
        {
            var authors = DataSeeder.GenerateAuthors(10);
            var genres = DataSeeder.GenerateGenres(5);
            var books = DataSeeder.GenerateBooks(100, authors, genres);

            await _authorService.AddRangeAsync(authors);
            await _genreService.AddRangeAsync(genres);
            await _bookService.AddRangeAsync(books);

            return RedirectToAction("Home", "Index");
        }
    }
}