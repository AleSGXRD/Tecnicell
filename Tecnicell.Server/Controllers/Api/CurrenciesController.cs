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
    public class CurrenciesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly CurrencyMapper _mapper;

        public CurrenciesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new CurrencyMapper(); 
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyViewModel>>> GetCurrencies()
        {
            return await _context.Currencies
                                .Select(model => _mapper.ToViewModel(model))
                                .ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{code}")]
        public async Task<ActionResult<CurrencyViewModel>> GetCurrency(string code)
        {
            var currency = await _context.Currencies
                                .Where(model => model.CurrencyCode == code)
                                .Select(model => _mapper.ToViewModel(model))
                                .FirstOrDefaultAsync();

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> PutCurrency(string code, CurrencyViewModel currency)
        {
            if (code != currency.CurrencyCode)
            {
                return BadRequest();
            }
            Currency model = _mapper.ToModel(currency);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(code))
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

        // POST: api/Currencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<CurrencyViewModel>> PostCurrency(CurrencyViewModel currency)
        {
            currency.CurrencyCode = GenerateCode();

            Currency model = _mapper.ToModel(currency);
            _context.Currencies.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CurrencyExists(currency.CurrencyCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCurrency", new { code = currency.CurrencyCode }, currency);
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> DeleteCurrency(string code)
        {
            var currency = await _context.Currencies.FindAsync(code);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (CurrencyExists(code) == false)
                    break;
            }
            return code;
        }
        private bool CurrencyExists(string code)
        {
            return _context.Currencies.Any(e => e.CurrencyCode == code);
        }
    }
}
