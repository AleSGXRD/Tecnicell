using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.AccessoryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Controllers.Api.Accessories
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryHistoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly AccessoryHistoryMapper _mapper;

        public AccessoryHistoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new AccessoryHistoryMapper();
        }

        // GET: api/AccessoryHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessoryHistoryViewModel>>> GetAccessoryHistories()
        {
            return await _context.AccessoryHistories
                .Include(history => history.SaleCodeNavigation)
                .Include(history => history.ToBranchNavigation)
                .Select(history => _mapper.ToViewModel(history))
                .ToListAsync();
        }

        // GET: api/AccessoryHistories/5
        [HttpGet("{code,date}")]
        public async Task<ActionResult<AccessoryHistoryViewModel>> GetAccessoryHistory(string code, DateTime date)
        {
            var accessoryHistory = await _context.AccessoryHistories
                .Include(history => history.SaleCodeNavigation)
                .Include(history => history.ToBranchNavigation)
                .Where(history => history.AccessoryCode == code && history.Date == date)
                .FirstOrDefaultAsync();

            if (accessoryHistory == null)
            {
                return NotFound();
            }

            return _mapper.ToViewModel(accessoryHistory);
        }

        // PUT: api/AccessoryHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code,date}")]
        public async Task<IActionResult> PutAccessoryHistory(string code, DateTime date, AccessoryHistoryViewModel accessoryHistory)
        {
            if (accessoryHistory.AccessoryCode != code || accessoryHistory.Date != date)
            {
                return BadRequest();
            }

            AccessoryHistory acHistory = _mapper.ToModel(accessoryHistory);

            _context.Entry(acHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoryHistoryExists(code,date))
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

        // POST: api/AccessoryHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessoryHistoryViewModel>> PostAccessoryHistory(AccessoryHistoryViewModel accessoryHistory)
        {
            _context.AccessoryHistories.Add(_mapper.ToModel(accessoryHistory));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccessoryHistoryExists(accessoryHistory.AccessoryCode, accessoryHistory.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccessoryHistory", new { code = accessoryHistory.AccessoryCode }, accessoryHistory);
        }

        // DELETE: api/AccessoryHistories/5
        [HttpDelete("{code,date}")]
        public async Task<IActionResult> DeleteAccessoryHistory(string code, DateTime date)
        {
            var accessoryHistory = await _context.AccessoryHistories
                                        .Where(history => history.AccessoryCode == code && history.Date == date)
                                        .FirstOrDefaultAsync();
            if (accessoryHistory == null)
            {
                return NotFound();
            }

            _context.AccessoryHistories.Remove(accessoryHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessoryHistoryExists(string code,DateTime date)
        {
            return _context.AccessoryHistories.Any(e => e.AccessoryCode == code && e.Date == date);
        }
    }
}
