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
using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.Responses;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Controllers.Api.Phones
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
    public class PhoneRepairsController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly PhoneRepairMapper _mapper;

        public PhoneRepairsController(TecnicellContext context)
        {
            _context = context;
            _mapper = new PhoneRepairMapper();
        }

        // GET: api/PhoneRepairs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneRepairView>>> GetPhoneRepairs()
        {
            return await _context.PhoneRepairViews.ToListAsync();
        }

        // GET: api/PhoneRepairs/5
        [HttpGet("{imei}")]
        public async Task<ActionResult<PhoneRepairResponse>> GetPhoneRepair(string imei)
        {
            var phone = await _context.PhoneRepairs
                                .Include(model => model.ImageCodeNavigation)
                                .Where(model => model.Imei == imei)
                                .FirstOrDefaultAsync();
            if (phone == null)
            {
                return NotFound();
            }
            var phoneView = await _context.PhoneRepairViews
                                    .Where(model => imei == model.Code)
                                    .FirstOrDefaultAsync();

            PhoneRepairHistoryMapper phoneHistoryMapper = new PhoneRepairHistoryMapper();
            var phoneHistories = await _context.PhoneRepairHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Include(model => model.UserCodeNavigation)
                .Where(model => model.Imei == imei)
                .OrderByDescending(model => model.Date)
                .Select(model => phoneHistoryMapper.ToViewModel(model))
                .ToListAsync();

            ImageMapper imageMapper = new ImageMapper();
            var Image = phone.ImageCodeNavigation != null ? imageMapper.ToViewModel(phone.ImageCodeNavigation) : null;

            PhoneRepairResponse response = new PhoneRepairResponse
            {
                Histories = phoneHistories,
                View = phoneView,
                Image = Image ?? null
            };

            return response;
        }

        // PUT: api/PhoneRepairs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{imei}")]
        public async Task<IActionResult> PutPhoneRepair(string imei, PhoneRepairViewModel phoneRepair)
        {
            if (imei != phoneRepair.Imei)
            {
                return BadRequest();
            }

            PhoneRepair model = _mapper.ToModel(phoneRepair);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneRepairExists(imei))
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

        // POST: api/PhoneRepairs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhoneRepairViewModel>> PostPhoneRepair(PhoneRepairViewModel phoneRepair)
        {
            PhoneRepair model = _mapper.ToModel(phoneRepair);

            _context.PhoneRepairs.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhoneRepairExists(phoneRepair.Imei))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhoneRepair", new { imei = phoneRepair.Imei }, phoneRepair);
        }

        // DELETE: api/PhoneRepairs/5
        [HttpDelete("{imei}")]
        public async Task<IActionResult> DeletePhoneRepair(string imei)
        {
            var phoneRepair = await _context.PhoneRepairs.FindAsync(imei);
            if (phoneRepair == null)
            {
                return NotFound();
            }

            _context.PhoneRepairs.Remove(phoneRepair);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneRepairExists(string imei)
        {
            return _context.PhoneRepairs.Any(e => e.Imei == imei);
        }
    }
}
