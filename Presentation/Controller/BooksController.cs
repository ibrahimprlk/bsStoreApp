using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IActionResult CreateBook([FromBody] Book book)
        {

            if (book is null)
                return BadRequest();

            _manager.BookService.CreateOneBook(book);

            // 201 Created + eklenen kaydın kendisi
            return CreatedAtAction(nameof(GetOneBook), new { id = book.Id }, book);

        }

        [HttpPut("[action]/{id:int}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book book)
        {

            if (book is null)
                return NotFound();

            _manager.BookService.UpdateOneBook(id, book, true);

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
