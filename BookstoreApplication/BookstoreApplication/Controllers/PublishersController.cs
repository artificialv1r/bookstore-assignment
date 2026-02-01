using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;

        public PublishersController(AppDbContext context)
        {
            _publisherService = new PublisherService(new PublisherRepository(context));
        }
        
        // GET: api/publishers
        [HttpGet]
        public async Task<ActionResult<List<Publisher>>> GetAll()
        {
            try
            {
                var publishers = await _publisherService.GetAll();
                return Ok(publishers);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetOne(int id)
        {
            try
            {
                var publisher = await _publisherService.GetOne(id);
                return Ok(publisher); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/publishers
        [HttpPost]
        public async Task<ActionResult<Publisher>> Post(Publisher publisher)
        {
            try
            {
                var createdPublisher = await _publisherService.Add(publisher);
                return Created(string.Empty, createdPublisher);
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

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Publisher>> Put(int id, Publisher publisher)
        {
            try
            {
                var updatedPublisher = await _publisherService.Update(publisher);
                return Ok(updatedPublisher);

            } 
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message); 
            }
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _publisherService.Delete(id);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message); 
            }
            
        }
    }
}
