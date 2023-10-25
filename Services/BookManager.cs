using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
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
            var entity = await BookCheck(id, trackChanges);
            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<BookDto> books, MetaData metadata)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges)
        {
            if (!bookParameters.ValidPriceRange)
                throw new PriceOutOFRangeBadRequestException();

            var booksWithMetaData = await _manager.Book
                .GetAllBooksAsync(bookParameters,trackChanges);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            return (booksDto, booksWithMetaData.MetaData);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await BookCheck(id, trackChanges);
            var entity = _mapper.Map<BookDto>(book);

            return entity;
        }
        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)>
             GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await BookCheck(id, trackChanges);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book);
        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }
        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            // check entity
            var entity = await BookCheck(id, trackChanges);
            entity = _mapper.Map<Book>(bookDto);
            _manager.Book.UpdateOneBook(entity);
            await _manager.SaveAsync(); 
        }
        private async Task<Book> BookCheck(int id , bool trackChanges)
        {
            // check entity
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);

            return entity;
        }
        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);
            return books;
        }
    }
}
