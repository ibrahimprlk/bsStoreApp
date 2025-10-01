using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controller
{
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
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }


        [HttpGet("[action]/{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = _manager.BookService.GetOneBookById(id, false);
            return Ok(book);
        }

        [HttpPost("[action]")]
        public IActionResult CreateBook([FromBody] BookDtoForInsertion bookDto)
        {

            if (bookDto is null)
                return BadRequest();

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = _manager.BookService.CreateOneBook(bookDto);

            return StatusCode(201, book);

        }

        [HttpPut("[action]/{id:int}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {

            if (bookDto is null)
                return NotFound();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _manager.BookService.UpdateOneBook(id, bookDto, false);

            return NoContent();
        }

        [HttpDelete("[action]/{id:int}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            _manager.BookService.DeleteOneBook(id, false);

            return NoContent();
        }

    }
}
