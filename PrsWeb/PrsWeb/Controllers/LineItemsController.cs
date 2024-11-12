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
    public class LineItemsController : ControllerBase
    {
        private readonly PrsdbContext _context;

        public LineItemsController(PrsdbContext context)
        {
            _context = context;
        }

        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItems()
        {

            return await _context.LineItems.Include(li => li.Request).Include(li => li.Product).ToListAsync();
        }

        // GET: api/LineItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItem>> GetLineItem(int id)
        {
            var lineItem = await _context.LineItems
                .Include(l => l.Request)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lineItem == null)
            {
                return NotFound();
            }

            return lineItem;
        }

        // PUT: api/LineItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<LineItem>> PutLineItem(int id, LineItem lineItem)
        {
            if (id != lineItem.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(lineItem).State = EntityState.Modified;            

            try
            {
                await _context.SaveChangesAsync();
                await recalcTotal(lineItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineItemExists(id))
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

        // POST: api/LineItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
        {
            Console.WriteLine("LineItem post: " + lineItem.RequestId + ", Product Id: " + lineItem.ProductId);
            nullifyAndSetId(lineItem);
            _context.LineItems.Add(lineItem);            
            await _context.SaveChangesAsync();
            await recalcTotal(lineItem);

            return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
        }

        private void nullifyAndSetId(LineItem lineItem)
        {
            if (lineItem.Request != null)
            {
                if (lineItem.RequestId == 0)
                {
                    lineItem.RequestId = lineItem.Request.Id;
                }
                lineItem.Request = null;
            }

            if (lineItem.Product != null)
            {
                if (lineItem.ProductId == 0)
                {
                    lineItem.ProductId = lineItem.Product.Id;
                }
                lineItem.Product = null;
            }

        }

        // DELETE: api/LineItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineItem(int id)
        {
            var lineItem = await _context.LineItems.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            _context.LineItems.Remove(lineItem);
            
            await _context.SaveChangesAsync();
            await recalcTotal(lineItem);

            return NoContent();
        }

        private bool LineItemExists(int id)
        {
            return _context.LineItems.Any(e => e.Id == id);
        }

        //GET: api/LineItems/lines-for-req/{reqId}
        [HttpGet("lines-for-req/{requestId}")]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLinesForRequestId(int requestId)
        {
            var lineitems = await _context.LineItems
                .Include(l => l.Request)
                .Include(l => l.Product)
                .ThenInclude(p => p.Vendor)
                .Where(l => l.RequestId == requestId)
                .ToListAsync();

            

            Console.WriteLine($"Found {lineitems.Count} line items for requestId: {requestId}"); 
            
            return lineitems;
        }

        public async Task<LineItem> recalcTotal(LineItem lineItem)
        {
            //Request request, Product product
            // SELECT total FROM LineItem Join Request and Product On each Id
            // Request.Total = Product.Price * LineItem.Quantity

            Product product = new Product();
            //Request request = lineItem.Request;

            var request = await _context.Requests
                .Include(r => r.LineItems)
                .FirstOrDefaultAsync(r => r.Id == lineItem.RequestId);

            var lineItems = await _context.LineItems
                .Include(l => l.Product)
                .Include(l => l.Request)
                .Where(l => l.RequestId == request.Id)
                .ToListAsync();

            // loop thru lineitems            
            decimal newRequestTotal = 0.0m;
            foreach (var item in lineItems)
            {
                // for each lineitem, calculate the linetotal and add to newReqeustTotal

                decimal lineTotal = item.Product.Price * item.Quantity;

                newRequestTotal += lineTotal;
            }
            request.Total = newRequestTotal;

            // save request
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return lineItem;


        }
    }
}
