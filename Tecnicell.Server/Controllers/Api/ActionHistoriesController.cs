using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
    public class ActionHistoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly ActionHistoryMapper _mapper;

        public ActionHistoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new ActionHistoryMapper();
        }

        // GET: api/ActionHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionHistoryViewModel>>> GetActionHistories()
        {
            return await _context.ActionHistories
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/ActionHistories/5
        [HttpGet("{name}")]
        public async Task<ActionResult<ActionHistoryViewModel>> GetActionHistory(string name)
        {
            var actionHistory = await _context.ActionHistories
                .Where(model => model.Name == name)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (actionHistory == null)
            {
                return NotFound();
            }

            return actionHistory;
        }

        // PUT: api/ActionHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutActionHistory(string name, ActionHistoryViewModel actionHistory)
        {
            if (name != actionHistory.Name)
            {
                return BadRequest();
            }

            ActionHistory model = _mapper.ToModel(actionHistory);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionHistoryExists(name))
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

        // POST: api/ActionHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActionHistoryViewModel>> PostActionHistory(ActionHistoryViewModel actionHistory)
        {
            ActionHistory model = _mapper.ToModel(actionHistory);

            _context.ActionHistories.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActionHistoryExists(actionHistory.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActionHistory", new { name = actionHistory.Name }, actionHistory);
        }

        // DELETE: api/ActionHistories/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteActionHistory(string name)
        {
            var actionHistory = await _context.ActionHistories.FindAsync(name);
            if (actionHistory == null)
            {
                return NotFound();
            }

            _context.ActionHistories.Remove(actionHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActionHistoryExists(string name)
        {
            return _context.ActionHistories.Any(e => e.Name == name);
        }
    }
}
