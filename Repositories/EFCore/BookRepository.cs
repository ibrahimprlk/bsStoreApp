using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {

        }

        public async Task CreateOneBookAsync(Book book)
        {
             await CreateAsync(book);
        }

        public void DeleteOneBook(Book book)
        {
            Delete(book);
        }

        public async Task<PagedList<Book>> GetAllAsync(BookParameters bookParameters,bool trackChanges)
        {
                var books = await FindAll(trackChanges)
                .OrderBy(x=>x.Id)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(books,bookParameters.PageNumber,bookParameters.PageSize);
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateOneBook(Book book)
        {
            Update(book);
        }
    }
}
