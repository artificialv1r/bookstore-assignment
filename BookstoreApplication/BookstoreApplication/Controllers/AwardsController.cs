using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AwardsController: ControllerBase
    {
        private readonly AwardRepository _awardRepository;

        public AwardsController(AwardRepository awardRepository)
        {
            _awardRepository = awardRepository;
        }
        
        // GET: api/awards
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_awardRepository.GetAll());
        }
        
        // GET api/awards/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var award = _awardRepository.GetOne(id);
            if (award == null)
            {
                return NotFound();
            }
            return Ok(award);
        }
        
        // POST api/awards
        [HttpPost]
        public IActionResult Post(Award award)
        {
            _awardRepository.Add(award);
            return Ok(award);
        }
        
        // PUT api/awards/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Award award)
        {
            if (id != award.Id)
            {
                return BadRequest();
            }
            
            return Ok(_awardRepository.Update(award));
        }
        
        // DELETE api/awards/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _awardRepository.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}