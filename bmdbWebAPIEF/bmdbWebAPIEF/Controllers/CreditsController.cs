using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bmdbWebAPIEF.Models;

namespace bmdbWebAPIEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditsController : ControllerBase
    {
        private readonly bmdbContext _context;

        public CreditsController(bmdbContext context)
        {
            _context = context;
        }

        // GET: api/Credits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credit>>> GetCredits()
        {
            return await _context.Credits.Include(c => c.Movie).Include(c => c.Actor).ToListAsync();
        }

        // GET: api/Credits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credit>> GetCredit(int id)
        {
            var credit = await _context.Credits.Include(c => c.Movie).Include(c => c.Actor).FirstOrDefaultAsync(c => c.Id == id);

            if (credit == null)
            {
                return NotFound();
            }

            return credit;
        }

        // PUT: api/Credits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCredit(int id, Credit credit)
        {
            if (id != credit.Id)
            {
                return BadRequest();
            }

            _context.Entry(credit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Credits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Credit>> PostCredit(Credit credit)
        {
            nullifyAndSetId(credit);
            _context.Credits.Add(credit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredit", new { id = credit.Id }, credit);
        }

        // Diff between Java and .Net projects:
        // Front End sends 'fully qualified' lineItem w/ request and product instances
        // These need to be nulled-out and the respective movieId and actorId needs to be set
        private void nullifyAndSetId(Credit credit)
        {
            if(credit.Movie != null)
            {
                if(credit.MovieId == 0)
                {
                    credit.MovieId = credit.Movie.Id;
                }
                credit.Movie = null;
            }

            if (credit.Actor != null)
            {
                if (credit.ActorId == 0)
                {
                    credit.ActorId = credit.Actor.Id;
                }
                credit.Actor = null;
            }

        }

        // DELETE: api/Credits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredit(int id)
        {
            var credit = await _context.Credits.FindAsync(id);
            if (credit == null)
            {
                return NotFound();
            }

            _context.Credits.Remove(credit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditExists(int id)
        {
            return _context.Credits.Any(e => e.Id == id);
        }

        // GET: api/Credits/movie/4
        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Credit>>> GetCreditsForMovieId(int movieId)
        {
            var credits = await _context.Credits
                .Include(c => c.Movie)
                .Include(c => c.Actor)
                .Where(c => c.MovieId == movieId)
                .ToListAsync();
            return credits;
        }

    }
}
