using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid AuthorId { get; set; }
        public Guid GenreId { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
