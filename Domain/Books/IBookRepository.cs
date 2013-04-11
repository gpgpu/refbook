﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books
{
    public interface IBookRepository : IRepository
    {
        IQueryable<Book> GetBooks(int portalId);
    }
}
