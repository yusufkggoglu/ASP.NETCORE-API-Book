using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggingService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<BookDtoForCreate> CreateOneBookAsync(BookDtoForCreate bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return bookDto;
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Book.GetOneBookByIdAsync(id,trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            var entity = _mapper.Map<BookDto>(book);
            if (entity is null)
                throw new BookNotFoundException(id);

            return entity;
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            // check entity
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.UpdateOneBook(entity);
            await _manager.SaveAsync(); 
        }
    }
}
