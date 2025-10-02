using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllAsync(bool trackChanges);
        Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
        Task CreateOneBookAsync(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
