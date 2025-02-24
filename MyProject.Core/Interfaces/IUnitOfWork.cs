﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Book> Books { get; }
        Task<int> SaveChangesAsync();
    }
}
