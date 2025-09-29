using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                var books = _manager.BookService.GetAllBooks(false);

                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("[action]/{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager.BookService.GetOneBookById(id, false);

                if (book is null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult CreateBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();

                _manager.BookService.CreateOneBook(book);

                // 201 Created + eklenen kaydın kendisi
                return CreatedAtAction(nameof(GetOneBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Kitap eklenirken bir hata oluştu.",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("[action]/{id:int}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return NotFound();

                _manager.BookService.UpdateOneBook(id, book, true);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("[action]/{id:int}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            try
            {
                _manager.BookService.DeleteOneBook(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
