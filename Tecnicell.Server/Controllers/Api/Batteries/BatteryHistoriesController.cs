using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.BatteryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Controllers.Api.Batteries
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteryHistoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly BatteryHistoryMapper _mapper;

        public BatteryHistoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new BatteryHistoryMapper();
        }

        // GET: api/BatteryHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatteryHistoryViewModel>>> GetBatteryHistories()
        {
            return await _context.BatteryHistories
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/BatteryHistories/5
        [HttpGet("{code, date}")]
        public async Task<ActionResult<BatteryHistoryViewModel>> GetBatteryHistory(string code, DateTime date)
        {
            var batteryHistory = await _context.BatteryHistories
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Where(model => model.BatteryCode == code && model.Date == date)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (batteryHistory == null)
            {
                return NotFound();
            }

            return batteryHistory;
        }

        // PUT: api/BatteryHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatteryHistory(string code, DateTime date, BatteryHistoryViewModel batteryHistory)
        {
            if (code != batteryHistory.BatteryCode || date != batteryHistory.Date)
            {
                return BadRequest();
            }

            BatteryHistory model = _mapper.ToModel(batteryHistory);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryHistoryExists(code,date))
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

        // POST: api/BatteryHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BatteryHistoryViewModel>> PostBatteryHistory(BatteryHistoryViewModel batteryHistory)
        {
            BatteryHistory model = _mapper.ToModel(batteryHistory);
            _context.BatteryHistories.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BatteryHistoryExists(batteryHistory.BatteryCode, batteryHistory.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBatteryHistory", new { code = batteryHistory.BatteryCode, date = batteryHistory.Date }, batteryHistory);
        }

        // DELETE: api/BatteryHistories/5
        [HttpDelete("{code, date}")]
        public async Task<IActionResult> DeleteBatteryHistory(string code, DateTime date)
        {
            var batteryHistory = await _context.BatteryHistories.Where(model => model.BatteryCode == code && model.Date == date).FirstOrDefaultAsync();
            if (batteryHistory == null)
            {
                return NotFound();
            }

            _context.BatteryHistories.Remove(batteryHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatteryHistoryExists(string code, DateTime date)
        {
            return _context.BatteryHistories.Any(e => e.BatteryCode == code && e.Date == date);
        }
    }
}
