using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class SalesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly SaleMapper _mapper;

        public SalesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new SaleMapper();
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleViewModel>>> GetSales()
        {
            return await _context.Sales
                .Include(model => model.CurrencyCodeNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{code}")]
        public async Task<ActionResult<SaleViewModel>> GetSale(string code)
        {
            var sale = await _context.Sales
                .Include(model => model.CurrencyCodeNavigation)
                .Where(model => model.SaleCode == code)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutSale(string code, SaleViewModel sale)
        {
            if (code != sale.SaleCode)
            {
                return BadRequest();
            }

            Sale model = _mapper.ToModel(sale);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(code))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPost]
        public async Task<ActionResult<SaleViewModel>> PostSale(SaleViewModel sale)
        {
            sale.CurrencyCode = GenerateCode();

            Sale model = _mapper.ToModel(sale);
            _context.Sales.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SaleExists(sale.SaleCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSale", new { code = sale.SaleCode }, sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteSale(string code)
        {
            var sale = await _context.Sales.FindAsync(code);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (SaleExists(code) == false)
                    break;
            }
            return code;
        }

        private bool SaleExists(string code)
        {
            return _context.Sales.Any(e => e.SaleCode == code);
        }
    }
}
