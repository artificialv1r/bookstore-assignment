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

        public BooksController(IBookService bookService, IAuthorService authorService,
            IPublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bookService.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            return Ok(await _bookService.GetById(id));
        }

        // POST api/books
        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

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

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.Delete(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
