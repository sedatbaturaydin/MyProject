using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Core.Entities;

namespace MyProject.Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }
        public int Page { get; set; }
        public Guid AuthorId { get; set; }
        public Guid GenreId { get; set; }
    }
}
