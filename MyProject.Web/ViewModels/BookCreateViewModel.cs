using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.Application.DTOs;
using MyProject.Core.Entities;

namespace MyProject.Web.ViewModels
{
    public class BookCreateViewModel
    {
        public BookDto Book { get; set; }
        public IEnumerable<SelectListItem>? Authors { get; set; }
        public IEnumerable<SelectListItem>? Genres { get; set; }
    }
}
