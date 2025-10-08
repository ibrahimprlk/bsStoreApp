using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controller
{
   // [ApiVersion("1.0")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    // [ResponseCache(CacheProfileName ="5mins")]
    //[HttpCacheExpiration(CacheLocation =CacheLocation.Public,MaxAge =80)]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize]
        [HttpHead]
        [HttpGet("[action]")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
      //  [ResponseCache(Duration =60)]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };

            var pagedResult = await _manager.BookService.GetAllBooksAsync(linkParameters,false);

            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));

            return pagedResult.linkResponse.HasLinks ?
                Ok(pagedResult.linkResponse.LinkedEntities) :
                Ok(pagedResult.linkResponse.ShapedEntities);
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

       // [Authorize]
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }

    }
}
