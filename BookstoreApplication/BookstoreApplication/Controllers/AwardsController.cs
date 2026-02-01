using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AwardsController: ControllerBase
    {
        private readonly AwardService _awardService;

        public AwardsController(AppDbContext context)
        {
            _awardService = new AwardService(new AwardRepository(context));
        }
        
        // GET: api/awards
        [HttpGet]
        public async Task<ActionResult<List<Award>>> GetAll()
        {
            try
            {
                var awards = await _awardService.GetAll();
                return Ok(awards);
            }catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
        
        // GET api/awards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Award>> GetOne(int id)
        {
            try
            {
                var award = await _awardService.GetOne(id);
                return Ok(award);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
        // POST api/awards
        [HttpPost]
        public async Task<ActionResult<Award>> Post(Award award)
        {
            try
            {
                var createdAward = await _awardService.Create(award);
                return Created(string.Empty, createdAward);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        // PUT api/awards/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Award>> Put(int id, Award award)
        {
            try
            {
                var updatedAward = await _awardService.Update(award);
                return Ok(updatedAward);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
        // DELETE api/awards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _awardService.Delete(id);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}