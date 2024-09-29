﻿using System;
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

            return await _context.LineItems.ToListAsync();
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
        public async Task<IActionResult> PutLineItem(int id, LineItem lineItem)
        {
            if (id != lineItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(lineItem).State = EntityState.Modified;

            try
            {
                await recalcTotal();
                await _context.SaveChangesAsync();
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
            _context.LineItems.Add(lineItem);
            await recalcTotal();
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
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
            await recalcTotal();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool LineItemExists(int id)
        {
            return _context.LineItems.Any(e => e.Id == id);
        }

        // GET: api/LineItems/lines-for-req/{reqId}
        [HttpGet("lines-for-req/{requestId}")]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLinesForRequestId(int requestId)
        {
            var lineitems = await _context.LineItems
                .Include(l => l.Request)
                .Include(l => l.Product)
                .Where(l => l.RequestId == requestId)
                .ToListAsync();
            return lineitems;
        }

        public async Task<decimal> recalcTotal()
        {
            //Request request, Product product
            // SELECT FROM LineItem Join Request/Product 
            // Request.Total = Product.Price * LineItem.Quantity

            Product product = new Product();
            LineItem lineItem = new LineItem();
            Request request = new Request();

            var lineItems = await _context.LineItems
                .Include(l => l.Request)
                .Include(l => l.Product)
                .Where(l => l.RequestId == request.Id && l.ProductId == product.Id)
                .ToListAsync();
            decimal total = lineItems.Sum(lineItems => lineItems.Product.Price * lineItems.Quantity);
            //decimal total = product.Price * lineItem.Quantity;            
            request.Total = total;
            return request.Total;
        }
    }
}
