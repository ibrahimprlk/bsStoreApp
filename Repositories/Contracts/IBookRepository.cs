using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllAsync(BookParameters bookParameters,bool trackChanges);
        Task<List<Book>> GetAllAsync(bool trackChanges);
        Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
        Task CreateOneBookAsync(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges);
        
    }
}
