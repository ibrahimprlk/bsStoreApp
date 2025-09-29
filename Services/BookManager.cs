using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    internal class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Book CreateOneBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _repositoryManager.Book.Create(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            _repositoryManager.Book.Delete(book);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _repositoryManager.Book.GetAll(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _repositoryManager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new Exception($"Book with id:{id} could not found.");
            }
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            entity.Title = book.Title;
            entity.Price = book.Price;
            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}
