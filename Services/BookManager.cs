using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    internal class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public Book CreateOneBook(Book book)
        {
            
            _repositoryManager.Book.Create(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            _repositoryManager.Book.Delete(book);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _repositoryManager.Book.GetAll(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return book;
        }

        public void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, false);
            if (entity is null)
                throw new BookNotFoundException(id);

            //mapping
            //entity.Title = book.Title;
            //entity.Price = book.Price;

            entity= _mapper.Map<Book>(bookDto);

            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}
