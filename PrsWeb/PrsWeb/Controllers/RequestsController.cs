using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsWeb.Models;

namespace PrsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {        
        private readonly PrsdbContext _context;

        public RequestsController(PrsdbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
                        
            request.SubmittedDate = DateTime.Now;
            
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            request.Status = "NEW";
            request.SubmittedDate = DateTime.Now;            
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }


        // PUT: api/Requests/approve/submit-review/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("submit-review/{id}")]
        public async Task<ActionResult<Request>> PutSubmitRequestForReview(int id)
        {
            // if total is <= $50, set request status to 'APPROVED', else set to "Review"
            // Update the request to REVIEW status (set request.status = "REVIEW"), then save it
            // Change submittedDate to current date
            var request = await _context.Requests.FindAsync(id);            

            if (request == null)
            {
                return NotFound();
            }
            else if (request.Total <= 50.00m)
            {
                request.Status = "APPROVED";                    
            }
            else
            {
                request.Status = "REVIEW";
            }   
            request.SubmittedDate = DateTime.Now;
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return request;
        }


        // GET: api/Requests/list-review/{userId}
        [HttpGet("list-review/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestReview(int userId)
        {
            // Get all requests
            // Wherestatus = "REVIEW" and req.userId != signed in user
            // Get requests in REVIEW status and req.userId != to userId

            var requests = await _context.Requests                
                .Include(r => r.User)
                .Where(r => r.UserId != userId && r.Status == "REVIEW")                
                .ToListAsync();
            if (requests == null)
            {
                return NotFound();
            }
            return requests;
        }
        

        // PUT: api/Requests/approve/{id}        
        [HttpPut("approve/{id}")]
        public async Task<ActionResult<Request>> PutRequestApprove(int id)
        {
            // get the request for id, set status to approved, and then save request.
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            else
            {
                request.Status = "APPROVED";
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return request;
        }


        // PUT: api/Requests/Reject/reject/{id}        
        [HttpPut("reject/{id}")]
        public async Task<ActionResult<Request>> PutRequestReject(int id)
        {
            // get the request for id, set status to REJECTED, set the ReasonForRejection, then save request
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            else
            {
                request.Status = "REJECTED";
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return request;
        }
    }
}
