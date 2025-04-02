using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Core;
using MyProject.Core.Interfaces;
using MyProject.Infrastructure.Data;
using MyProject.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MyProject.Infrastructure.Repositories
{
    public class AuthorRepository(AppDbContext context) : GenericRepository<Author>(context), IAuthorRepository
    {
    }
}
