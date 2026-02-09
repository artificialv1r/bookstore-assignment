using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.Interfaces;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService ;
        }
        
        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAll()
        {
            try
            {
                var authors = await _authorService.GetAll();
                return Ok(authors);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetOne(int id)
        {
            try
            {
                var author = await _authorService.GetOne(id);
                return Ok(author);
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

        // POST api/authors
        [HttpPost]
        public async Task<ActionResult<Author>> Post(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var createdAuthor = await _authorService.Create(author);
            return Created(string.Empty, createdAuthor);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> Put(int id, Author author)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            var updatedAuthor = await _authorService.Update(author);
            return Ok(updatedAuthor);
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _authorService.Delete(id);
            if (!success) 
            { 
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("paging")]
        public async Task<ActionResult<PaginatedList<Author>>> GetAuthorsPage([FromQuery] int page = 1)
        {
            if (page < 1)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authorService.GetAuthorsPage(page));
        }
    }
}
