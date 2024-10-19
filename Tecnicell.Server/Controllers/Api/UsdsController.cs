using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsdsController : ControllerBase
    {
        private readonly TecnicellContext _context;

        public UsdsController(TecnicellContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usd>>> GetUsd()
        {
            return await _context.Usds.ToListAsync();
        }

        // GET: api/Branches/5
        [HttpGet("{date}")]
        public async Task<ActionResult<Usd>> GetUsd(DateTime date)
        {
            var usd = await _context.Usds
                .Where(model => model.Date == date)
                .FirstOrDefaultAsync();

            if (usd == null)
            {
                return NotFound();
            }

            return usd;
        }
        // GET: api/Branches/5
        [HttpGet("last")]
        public async Task<ActionResult<Usd>> GetLastUsd()
        {
            var usd = await _context.Usds
                .OrderByDescending(model => model.Date)
                .FirstOrDefaultAsync();

            if (usd == null)
            {
                return NotFound();
            }

            return usd;
        }

        // PUT: api/Branches/5
        // To protect date overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutUsd(DateTime date, Usd usd)
        {
            if (date != usd.Date)
            {
                return BadRequest();
            }

            _context.Entry(usd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsdExists(date))
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

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usd>> PostUsd(Usd usd)
        {
            _context.Usds.Add(usd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsdExists(usd.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsd", new { date = usd.Date }, usd);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{date}")]
        public async Task<IActionResult> DeleteUsd(DateTime date)
        {
            var usd = await _context.Usds.FindAsync(date);
            if (usd == null)
            {
                return NotFound();
            }

            _context.Usds.Remove(usd);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool UsdExists(DateTime date)
        {
            return _context.Usds.Any(e => e.Date == date);
        }
    }
}
