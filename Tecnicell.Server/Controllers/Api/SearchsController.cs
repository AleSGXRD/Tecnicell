using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchsController : ControllerBase
    {
        private readonly TecnicellContext _context;

        public SearchsController(TecnicellContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<IEnumerable<Search>>> GetSearch()
        {
            return await _context.Searchs.ToListAsync();
        }

        // GET: api/Branches/5
        [HttpGet("{date}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<Search>> GetSearch(DateTime date)
        {
            var Search = await _context.Searchs
                .Where(model => model.Date == date)
                .FirstOrDefaultAsync();

            if (Search == null)
            {
                return NotFound();
            }

            return Search;
        }
        // GET: api/Branches/5
        [HttpGet("last")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<Search>> GetLastSearch()
        {
            var Search = await _context.Searchs
                .OrderByDescending(model => model.Date)
                .FirstOrDefaultAsync();

            if (Search == null)
            {
                return NotFound();
            }

            return Search;
        }

        // PUT: api/Branches/5
        // To protect date overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> PutSearch(DateTime date, Search Search)
        {
            if (date != Search.Date)
            {
                return BadRequest();
            }

            _context.Entry(Search).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchExists(date))
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
        public async Task<ActionResult<Search>> PostSearch(Search Search)
        {
            _context.Searchs.Add(Search);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SearchExists(Search.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSearch", new { date = Search.Date }, Search);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{date}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> DeleteSearch(DateTime date)
        {
            var Search = await _context.Searchs.FindAsync(date);
            if (Search == null)
            {
                return NotFound();
            }

            _context.Searchs.Remove(Search);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool SearchExists(DateTime date)
        {
            return _context.Searchs.Any(e => e.Date == date);
        }
    }
}
