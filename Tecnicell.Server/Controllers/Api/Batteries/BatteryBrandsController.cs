using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.BatteryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Controllers.Api.Batteries
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteryBrandsController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly BatteryBrandMapper _mapper;

        public BatteryBrandsController(TecnicellContext context)
        {
            _context = context;
            _mapper = new BatteryBrandMapper();
        }

        // GET: api/BatteryBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatteryBrandViewModel>>> GetBatteryBrands()
        {
            return await _context.BatteryBrands
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/BatteryBrands/5
        [HttpGet("{name}")]
        public async Task<ActionResult<BatteryBrandViewModel>> GetBatteryBrand(string name)
        {
            var batteryBrand = await _context.BatteryBrands
                .Where(model => model.Name == name)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (batteryBrand == null)
            {
                return NotFound();
            }

            return batteryBrand;
        }

        // PUT: api/BatteryBrands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutBatteryBrand(string name, BatteryBrandViewModel batteryBrand)
        {
            if (name != batteryBrand.Name)
            {
                return BadRequest();
            }

            BatteryBrand model = _mapper.ToModel(batteryBrand);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryBrandExists(name))
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

        // POST: api/BatteryBrands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BatteryBrandViewModel>> PostBatteryBrand(BatteryBrandViewModel batteryBrand)
        {
            BatteryBrand model = _mapper.ToModel(batteryBrand);
            _context.BatteryBrands.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BatteryBrandExists(batteryBrand.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBatteryBrand", new { name = batteryBrand.Name }, batteryBrand);
        }

        // DELETE: api/BatteryBrands/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteBatteryBrand(string name)
        {
            var batteryBrand = await _context.BatteryBrands
                                        .Where(model => model.Name == name)
                                        .FirstOrDefaultAsync();
            if (batteryBrand == null)
            {
                return NotFound();
            }

            _context.BatteryBrands.Remove(batteryBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatteryBrandExists(string name)
        {
            return _context.BatteryBrands.Any(e => e.Name == name);
        }
    }
}
