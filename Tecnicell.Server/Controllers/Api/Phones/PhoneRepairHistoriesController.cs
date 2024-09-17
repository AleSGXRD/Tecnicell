using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Controllers.Api.Phones
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneRepairHistoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly PhoneRepairHistoryMapper _mapper;

        public PhoneRepairHistoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new PhoneRepairHistoryMapper();  
        }

        // GET: api/PhoneRepairHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneRepairHistoryViewModel>>> GetPhoneRepairHistories()
        {
            return await _context.PhoneRepairHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/PhoneRepairHistories/5
        [HttpGet("{imei, date}")]
        public async Task<ActionResult<PhoneRepairHistoryViewModel>> GetPhoneRepairHistory(string imei, DateTime date)
        {
            var phoneRepairHistory = await _context.PhoneRepairHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Where(model => model.Imei == imei && model.Date == date)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (phoneRepairHistory == null)
            {
                return NotFound();
            }

            return phoneRepairHistory;
        }

        // PUT: api/PhoneRepairHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{imei, date}")]
        public async Task<IActionResult> PutPhoneRepairHistory(string imei, DateTime date, PhoneRepairHistoryViewModel phoneRepairHistory)
        {
            if (imei != phoneRepairHistory.Imei || date != phoneRepairHistory.Date)
            {
                return BadRequest();
            }

            PhoneRepairHistory model = _mapper.ToModel(phoneRepairHistory);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneRepairHistoryExists(imei, date))
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

        // POST: api/PhoneRepairHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhoneRepairHistoryViewModel>> PostPhoneRepairHistory(PhoneRepairHistoryViewModel phoneRepairHistory)
        {
            PhoneRepairHistory model = _mapper.ToModel(phoneRepairHistory);

            _context.PhoneRepairHistories.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhoneRepairHistoryExists(phoneRepairHistory.Imei, phoneRepairHistory.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhoneRepairHistory", new { imei = phoneRepairHistory.Imei, date = phoneRepairHistory.Date }, phoneRepairHistory);
        }

        // DELETE: api/PhoneRepairHistories/5
        [HttpDelete("{imei, date}")]
        public async Task<IActionResult> DeletePhoneRepairHistory(string imei, DateTime date)
        {
            var phoneRepairHistory = await _context.PhoneRepairHistories
                                            .Where(model => model.Imei == imei && model.Date == date)
                                            .FirstOrDefaultAsync();
            if (phoneRepairHistory == null)
            {
                return NotFound();
            }

            _context.PhoneRepairHistories.Remove(phoneRepairHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneRepairHistoryExists(string imei, DateTime date)
        {
            return _context.PhoneRepairHistories.Any(e => e.Imei == imei && e.Date == date);
        }
    }
}
