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
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
    public class BrandsController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly BrandMapper _mapper;

        public BrandsController(TecnicellContext context)
        {
            _context = context;
            _mapper = new BrandMapper();
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandViewModel>>> GetBrands()
        {
            return await _context.Brands
                            .Select(brand => _mapper.ToViewModel(brand))
                            .ToListAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{name}")]
        public async Task<ActionResult<BrandViewModel>> GetBrand(string name)
        {
            var brand = await _context.Brands
                                    .Where(brand => brand.Name == name)
                                    .Select(brand=> _mapper.ToViewModel(brand))
                                    .FirstOrDefaultAsync();

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutBrand(string name, BrandViewModel brand)
        {
            if (name != brand.Name)
            {
                return BadRequest();
            }

            Brand model = _mapper.ToModel(brand);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(name))
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

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandViewModel>> PostBrand(BrandViewModel brand)
        {
            Brand model = _mapper.ToModel(brand);

            _context.Brands.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BrandExists(brand.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBrand", new { name = brand.Name }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteBrand(string name)
        {
            var brand = await _context.Brands.FindAsync(name);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(string name)
        {
            return _context.Brands.Any(e => e.Name == name);
        }
    }
}
