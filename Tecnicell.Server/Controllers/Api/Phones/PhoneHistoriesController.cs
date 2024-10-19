using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
    public class PhoneHistoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly PhoneHistoryMapper _mapper;

        public PhoneHistoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new PhoneHistoryMapper();
        }

        // GET: api/PhoneHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneHistoryViewModel>>> GetPhoneHistories()
        {
            return await _context.PhoneHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Include(model => model.UserCodeNavigation)
                .OrderByDescending(model => model.Date)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/PhoneHistories/5
        [HttpGet("{imei}")]
        public async Task<ActionResult<IEnumerable<PhoneHistoryViewModel>>> GetPhoneHistory(string imei)
        {
            var phoneHistory = await _context.PhoneHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Include(model => model.UserCodeNavigation)
                .Where(model => model.Imei == imei)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();

            if (phoneHistory == null)
            {
                return NotFound();
            }

            return phoneHistory;
        }

        // PUT: api/PhoneHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{imei, date}")]
        public async Task<IActionResult> PutPhoneHistory(string imei, DateTime date, PhoneHistoryViewModel phoneHistory)
        {
            if (imei != phoneHistory.Imei || date != phoneHistory.Date)
            {
                return BadRequest();
            }

            PhoneHistory model = _mapper.ToModel(phoneHistory);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneHistoryExists(imei,date))
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

        // POST: api/PhoneHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhoneHistoryViewModel>> PostPhoneHistory(PhoneHistoryViewModel phoneHistory)
        {
            PhoneHistory model = _mapper.ToModel(phoneHistory);
            _context.PhoneHistories.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhoneHistoryExists(phoneHistory.Imei, phoneHistory.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhoneHistory", new { imei = phoneHistory.Imei, date = phoneHistory.Date }, phoneHistory);
        }

        // DELETE: api/PhoneHistories/5
        [HttpDelete("{imei, date}")]
        public async Task<IActionResult> DeletePhoneHistory(string imei, DateTime date)
        {
            var phoneHistory = await _context.PhoneHistories
                                            .Where(model => model.Imei == imei && model.Date == date)
                                            .FirstOrDefaultAsync();
            if (phoneHistory == null)
            {
                return NotFound();
            }

            _context.PhoneHistories.Remove(phoneHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneHistoryExists(string imei, DateTime date)
        {
            return _context.PhoneHistories.Any(e => e.Imei == imei && e.Date == date);
        }
    }
}
