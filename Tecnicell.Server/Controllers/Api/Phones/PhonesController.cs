using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper;
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
        public async Task<ActionResult<IEnumerable<PhoneView>>> GetPhones()
        {
            return await _context.PhoneViews.ToListAsync();
        }

        // GET: api/Phones/5
        [HttpGet("{imei}")]
        public async Task<ActionResult<PhoneResponse>> GetPhone(string imei)
        {
            var phone = await _context.Phones
                                .Include(model => model.ImageCodeNavigation)
                                .Where(model => model.Imei == imei)
                                .FirstOrDefaultAsync();
            if (phone == null)
            {
                return NotFound();
            }
            var phoneView = await _context.PhoneViews
                                    .Where(model => imei == model.Code)
                                    .FirstOrDefaultAsync();

            PhoneHistoryMapper phoneHistoryMapper = new PhoneHistoryMapper();
            var phoneHistories = await _context.PhoneHistories
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

            PhoneResponse response = new PhoneResponse
            {
                Histories = phoneHistories,
                View = phoneView,
                Image = Image ?? null
            };

            return response;
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
