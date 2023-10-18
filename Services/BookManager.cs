using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggingService _logger;
        public BookManager(IRepositoryManager manager, ILoggingService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id,trackChanges);
            if (entity is null)
            {
                string message = $"The Book with id:{id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message); 
            }
            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _manager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                string message = $"Book with id:{id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            entity.Name = book.Name;
            entity.Price = book.Price;
            _manager.Book.UpdateOneBook(entity);
            _manager.Save(); 
        }
    }
}
