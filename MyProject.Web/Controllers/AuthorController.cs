using Microsoft.AspNetCore.Mvc;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Web.ViewModels;

namespace MyProject.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var authorDto=new AuthorDto
                {
                    Id = Guid.NewGuid(),
                    Name = model.Author.Name
                };
                await _authorService.AddAsync(authorDto);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
