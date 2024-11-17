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
    public class ImagesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly ImageMapper _mapper;

        public ImagesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new ImageMapper();    
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageViewModel>>> GetImages()
        {
            return await _context.Images
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{code}")]
        public async Task<ActionResult<ImageViewModel>> GetImage(string code)
        {
            var image = await _context.Images
                .Where(model => model.ImageCode == code)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutImage(string code, ImageViewModel image)
        {
            if (code != image.Imagecode)
            {
                return BadRequest();
            }

            Image model = _mapper.ToModel(image);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(code))
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

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPost]
        public async Task<ActionResult<ImageViewModel>> PostImage(ImageViewModel image)
        {
            image.Imagecode = GenerateCode();

            Image model = _mapper.ToModel(image);
            _context.Images.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageExists(image.Imagecode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImage", new { code = image.Imagecode }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteImage(string code)
        {
            var image = await _context.Images.FindAsync(code);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (ImageExists(code) == false)
                    break;
            }
            return code;
        }
        private bool ImageExists(string code)
        {
            return _context.Images.Any(e => e.ImageCode == code);
        }
    }
}
