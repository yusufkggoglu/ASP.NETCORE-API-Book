using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentations.Controllers
{
    [ApiController]
    [Route("api/books")]
    [ApiExplorerSettings(GroupName ="v2")]
    public class BookV2Controller :ControllerBase
    {
        private readonly IServiceManager _manager;
        public BookV2Controller(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {

            var books = await _manager.BookService.GetAllBooksAsync(false);
            var booksV2 = books.Select(b => new
            {
                Name = b.Name,
                Id = b.Id
            });
            return Ok(booksV2);
        }
    }
}
