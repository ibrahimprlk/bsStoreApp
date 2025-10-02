using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controller
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);
        }

      
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);
            return Ok(book);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookDtoForInsertion bookDto)
        {

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);
            return StatusCode(201, book);

        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
            return NoContent();
        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);

            return NoContent();
        }

    }
}
