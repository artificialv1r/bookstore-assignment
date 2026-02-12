using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Services.Interfaces;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        
        // GET: api/publishers
        [HttpGet]
        public async Task<ActionResult<List<Publisher>>> GetAll()
        {
            var publishers = await _publisherService.GetAll();
            return Ok(publishers);
            
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetOne(int id)
        {
            var publisher = await _publisherService.GetOne(id);
            return Ok(publisher); 
        }

        // POST api/publishers
        [HttpPost]
        public async Task<ActionResult<Publisher>> Post(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var createdPublisher = await _publisherService.Add(publisher);
            return Created(string.Empty, createdPublisher);
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Publisher>> Put(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            
            var updatedPublisher = await _publisherService.Update(publisher);
            return Ok(updatedPublisher);
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _publisherService.Delete(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("sorting")]
        public async Task<ActionResult<PaginatedList<Publisher>>> GetPublishersSorted([FromQuery] int page = 1,
            [FromQuery] PublisherSortType sortType = PublisherSortType.NameAsc)
        {
            if (page < 1)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await _publisherService.GetAllSorted(page, sortType));
        }
    }
}
