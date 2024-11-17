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
    public class SuppliersController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly SupplierMapper _mapper;

        public SuppliersController(TecnicellContext context)
        {
            _context = context;
            _mapper = new SupplierMapper();
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetSuppliers()
        {
            return await _context.Suppliers
                                .Select(model => _mapper.ToViewModel(model))
                                .ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{code}")]
        public async Task<ActionResult<SupplierViewModel>> GetSupplier(string code)
        {
            var Supplier = await _context.Suppliers
                                .Where(model => model.SupplierCode == code)
                                .Select(model => _mapper.ToViewModel(model))
                                .FirstOrDefaultAsync();

            if (Supplier == null)
            {
                return NotFound();
            }

            return Supplier;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> PutSupplier(string code, SupplierViewModel viewmodel)
        {
            if (code != viewmodel.SupplierCode)
            {
                return BadRequest();
            }
            Supplier model = _mapper.ToModel(viewmodel);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(code))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<SupplierViewModel>> PostSupplier(SupplierViewModel viewmodel)
        {
            viewmodel.SupplierCode = GenerateCode();

            Supplier model = _mapper.ToModel(viewmodel);
            _context.Suppliers.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupplierExists(viewmodel.SupplierCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupplier", new { code = viewmodel.SupplierCode }, viewmodel);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> DeleteSupplier(string code)
        {
            var model = await _context.Suppliers.FindAsync(code);
            if (model == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (SupplierExists(code) == false)
                    break;
            }
            return code;
        }
        private bool SupplierExists(string code)
        {
            return _context.Suppliers.Any(e => e.SupplierCode == code);
        }
    }
}
