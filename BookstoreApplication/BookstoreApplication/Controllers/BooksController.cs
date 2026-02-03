using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BooksController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
        }
        // GET: api/books
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _bookService.GetAll());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            try
            {
                return Ok(await _bookService.GetById(id));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // POST api/books
        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            try
            {
                var author = await _authorService.GetOne(book.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }
                
                var publisher = await _publisherService.GetOne(book.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }
                book.Author = author;
                book.Publisher = publisher;

                var createdBook = await _bookService.Create(book);
                return Created(string.Empty, createdBook);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest();
                }
                
                var author = await _authorService.GetOne(book.AuthorId);
                if (author == null)
                {
                    return BadRequest();
                }

                // izmena knjige je moguca ako je izabran postojeći izdavač
                var publisher = await _publisherService.GetOne(book.PublisherId);
                if (publisher == null)
                {
                    return BadRequest();
                }

                book.Author = author;
                book.Publisher = publisher;
                
                var updatedBook = await _bookService.Update(book);
                return Ok(updatedBook);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _bookService.Delete(id);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            
        }
    }
}
