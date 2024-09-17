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
    public class PhoneBrandsController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly PhoneBrandMapper _mapper;
        public PhoneBrandsController(TecnicellContext context)
        {
            _context = context;
            _mapper = new PhoneBrandMapper();
        }

        // GET: api/PhoneBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneBrandViewModel>>> GetPhoneBrands()
        {
            return await _context.PhoneBrands
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/PhoneBrands/5
        [HttpGet("{name}")]
        public async Task<ActionResult<PhoneBrandViewModel>> GetPhoneBrand(string name)
        {
            var phoneBrand = await _context.PhoneBrands
                .Where(model => model.Name == name)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (phoneBrand == null)
            {
                return NotFound();
            }

            return phoneBrand;
        }

        // PUT: api/PhoneBrands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutPhoneBrand(string name, PhoneBrandViewModel phoneBrand)
        {
            if (name != phoneBrand.Name)
            {
                return BadRequest();
            }

            PhoneBrand model = _mapper.ToModel(phoneBrand);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneBrandExists(name))
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

        // POST: api/PhoneBrands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhoneBrandViewModel>> PostPhoneBrand(PhoneBrandViewModel phoneBrand)
        {
            PhoneBrand model = _mapper.ToModel(phoneBrand);

            _context.PhoneBrands.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhoneBrandExists(phoneBrand.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhoneBrand", new { name = phoneBrand.Name }, phoneBrand);
        }

        // DELETE: api/PhoneBrands/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeletePhoneBrand(string name)
        {
            var phoneBrand = await _context.PhoneBrands
                                        .Where(model => model.Name == name)
                                        .FirstOrDefaultAsync();
            if (phoneBrand == null)
            {
                return NotFound();
            }

            _context.PhoneBrands.Remove(phoneBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneBrandExists(string name)
        {
            return _context.PhoneBrands.Any(e => e.Name == name);
        }
    }
}
