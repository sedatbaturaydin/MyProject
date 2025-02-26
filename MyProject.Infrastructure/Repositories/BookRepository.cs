using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Core;
using MyProject.Core.Interfaces;
using MyProject.Infrastructure.Data;
using MyProject.Core.Entities;

namespace MyProject.Infrastructure.Repositories
{
    public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
    {
    }
}
