using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.ScreenMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Screen;

namespace Tecnicell.Server.Controllers.Api.Screens
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenHistoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly ScreenHistoryMapper _mapper;

        public ScreenHistoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new ScreenHistoryMapper();
        }

        // GET: api/ScreenHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenHistoryViewModel>>> GetScreenHistories()
        {
            return await _context.ScreenHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/ScreenHistories/5
        [HttpGet("{code, date}")]
        public async Task<ActionResult<ScreenHistoryViewModel>> GetScreenHistory(string code, DateTime date)
        {
            var screenHistory = await _context.ScreenHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Where(model => model.ScreenCode == code && model.Date == date)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (screenHistory == null)
            {
                return NotFound();
            }

            return screenHistory;
        }

        // PUT: api/ScreenHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code, date}")]
        public async Task<IActionResult> PutScreenHistory(string code, DateTime date, ScreenHistoryViewModel screenHistory)
        {
            if (code != screenHistory.ScreenCode || date != screenHistory.Date)
            {
                return BadRequest();
            }

            ScreenHistory model = _mapper.ToModel(screenHistory);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenHistoryExists(code,date))
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

        // POST: api/ScreenHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScreenHistoryViewModel>> PostScreenHistory(ScreenHistoryViewModel screenHistory)
        {
            ScreenHistory model =_mapper.ToModel(screenHistory) ;
            _context.ScreenHistories.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScreenHistoryExists(screenHistory.ScreenCode, screenHistory.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScreenHistory", new { code = screenHistory.ScreenCode, date = screenHistory.Date }, screenHistory);
        }

        // DELETE: api/ScreenHistories/5
        [HttpDelete("{code, date}")]
        public async Task<IActionResult> DeleteScreenHistory(string code, DateTime date)
        {
            var screenHistory = await _context.ScreenHistories
                                                .Where(model => model.ScreenCode == code && model.Date == date)
                                                .FirstOrDefaultAsync();
            if (screenHistory == null)
            {
                return NotFound();
            }

            _context.ScreenHistories.Remove(screenHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScreenHistoryExists(string code, DateTime date)
        {
            return _context.ScreenHistories.Any(e => e.ScreenCode == code && e.Date == date);
        }
    }
}
