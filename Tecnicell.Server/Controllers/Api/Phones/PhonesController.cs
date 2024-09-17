using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Controllers.Api.Phones
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly PhoneMapper _mapper;

        public PhonesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new PhoneMapper();
        }

        // GET: api/Phones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneViewModel>>> GetPhones()
        {
            return await _context.Phones
                .Include(model => model.BrandNavigation)
                .Include(model => model.PhoneHistories)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/Phones/5
        [HttpGet("{imei}")]
        public async Task<ActionResult<PhoneViewModel>> GetPhone(string imei)
        {
            var phone = await _context.Phones
                                    .Include(model => model.BrandNavigation)
                                    .Include(model => model.PhoneHistories)
                                    .Where(model => model.Imei == imei)
                                    .Select(model => _mapper.ToViewModel(model))
                                    .FirstOrDefaultAsync();

            if (phone == null)
            {
                return NotFound();
            }

            return phone;
        }

        // PUT: api/Phones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{imei}")]
        public async Task<IActionResult> PutPhone(string imei, PhoneViewModel phone)
        {
            if (imei != phone.Imei)
            {
                return BadRequest();
            }

            Phone model = _mapper.ToModel(phone);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(imei))
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

        // POST: api/Phones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhoneViewModel>> PostPhone(PhoneViewModel phone)
        {
            Phone model = _mapper.ToModel(phone);

            _context.Phones.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhoneExists(phone.Imei))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhone", new { imei = phone.Imei }, phone);
        }

        // DELETE: api/Phones/5
        [HttpDelete("{imei}")]
        public async Task<IActionResult> DeletePhone(string imei)
        {
            var phone = await _context.Phones.FindAsync(imei);
            if (phone == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneExists(string imei)
        {
            return _context.Phones.Any(e => e.Imei == imei);
        }
    }
}
