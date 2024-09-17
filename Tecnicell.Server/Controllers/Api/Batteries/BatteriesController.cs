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
    public class BatteriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly BatteryMapper _mapper;

        public BatteriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new BatteryMapper();
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatteryViewModel>>> GetBatteries()
        {
            return await _context.Batteries
                .Include(model => model.BatteryHistories)
                .Include(model => model.BrandNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/Batteries/5
        [HttpGet("{code}")]
        public async Task<ActionResult<BatteryViewModel>> GetBattery(string code)
        {
            var battery = await _context.Batteries
                .Include(model => model.BatteryHistories)
                .Include(model => model.BrandNavigation)
                .Where(model => model.BatteryCode == code)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

        // PUT: api/Batteries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutBattery(string code, BatteryViewModel battery)
        {
            if (code != battery.BatteryCode)
            {
                return BadRequest();
            }

            _context.Entry(battery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryExists(code))
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

        // POST: api/Batteries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BatteryViewModel>> PostBattery(BatteryViewModel battery)
        {
            battery.BatteryCode = GenerateCode();

            Battery model = _mapper.ToModel(battery);
            _context.Batteries.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BatteryExists(battery.BatteryCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBattery", new { code = battery.BatteryCode }, battery);
        }

        // DELETE: api/Batteries/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteBattery(string code)
        {
            var battery = await _context.Batteries.FindAsync(code);
            if (battery == null)
            {
                return NotFound();
            }

            _context.Batteries.Remove(battery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (BatteryExists(code) == false)
                    break;
            }
            return code;
        }
        private bool BatteryExists(string code)
        {
            return _context.Batteries.Any(e => e.BatteryCode == code);
        }
    }
}
