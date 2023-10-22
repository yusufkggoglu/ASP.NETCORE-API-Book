using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
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
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
             var books = await _manager.BookService.GetAllBooksAsync(false);
             return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
        {
             var book = await _manager
                .BookService
                .GetOneBookByIdAsync(id, false);

             return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBook([FromBody] BookDtoForCreate bookDto)
        {
            if (bookDto is null)
                 return BadRequest(); // 400 

            await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, bookDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] BookDtoForUpdate bookDto)
        {
           
            if (bookDto is null)
                 return BadRequest(); // 400 

            await _manager.BookService.UpdateOneBookAsync(id, bookDto, true);
             return NoContent(); // 204
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBook([FromRoute(Name = "id")] int id)
        { 
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
                // check entity
                var entity = await _manager
                    .BookService
                    .GetOneBookByIdAsync(id, true);

            await _manager.BookService.UpdateOneBookAsync(id,
                 new BookDtoForUpdate(entity.Id, entity.Name, entity.Price),
                 true);

            return NoContent(); // 204
        }
    }
}
