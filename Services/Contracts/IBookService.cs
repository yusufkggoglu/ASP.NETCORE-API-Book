using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges); //ıenumerable , foreach ile ulaşabilmek için kullanıldı
        Task<BookDto> GetOneBookByIdAsync(int id,bool trackChanges);
        Task<BookDtoForCreate> CreateOneBookAsync(BookDtoForCreate bookDto); 
        Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto,bool trackChanges);
        Task DeleteOneBookAsync(int id,bool trackChanges);

        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);
    }
}
