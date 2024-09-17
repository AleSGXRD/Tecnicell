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
        public async Task<ActionResult<IEnumerable<PhoneRepairViewModel>>> GetPhoneRepairs()
        {
            return await _context.PhoneRepairs
                .Include(model => model.BrandNavigation)
                .Include(model => model.PhoneRepairHistories)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/PhoneRepairs/5
        [HttpGet("{imei}")]
        public async Task<ActionResult<PhoneRepairViewModel>> GetPhoneRepair(string imei)
        {
            var phoneRepair = await _context.PhoneRepairs
                .Include(model => model.BrandNavigation)
                .Include(model => model.PhoneRepairHistories)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (phoneRepair == null)
            {
                return NotFound();
            }

            return phoneRepair;
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
