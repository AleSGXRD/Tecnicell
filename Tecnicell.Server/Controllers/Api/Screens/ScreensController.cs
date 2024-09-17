using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.ScreenMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Screen;

namespace Tecnicell.Server.Controllers.Api.Screens
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly ScreenMapper _mapper;

        public ScreensController(TecnicellContext context)
        {
            _context = context;
            _mapper = new ScreenMapper();
        }

        // GET: api/Screens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenViewModel>>> GetScreens()
        {
            return await _context.Screens
                        .Include(model => model.BrandNavigation)
                        .Include(model => model.ScreenHistories)
                        .Select(model => _mapper.ToViewModel(model))
                        .ToListAsync();
        }

        // GET: api/Screens/5
        [HttpGet("{code}")]
        public async Task<ActionResult<ScreenViewModel>> GetScreen(string code)
        {
            var screen = await _context.Screens
                        .Include(model => model.BrandNavigation)
                        .Include(model => model.ScreenHistories)
                        .Where(model => model.ScreenCode == code)
                        .Select(model => _mapper.ToViewModel(model))
                        .FirstOrDefaultAsync();

            if (screen == null)
            {
                return NotFound();
            }

            return screen;
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutScreen(string code, ScreenViewModel screen)
        {
            if (code != screen.ScreenCode)
            {
                return BadRequest();
            }

            Screen model = _mapper.ToModel(screen);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenExists(code))
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

        // POST: api/Screens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScreenViewModel>> PostScreen(ScreenViewModel screen)
        {
            screen.ScreenCode = GenerateCode();

            Screen model = _mapper.ToModel(screen);
            _context.Screens.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScreenExists(screen.ScreenCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScreen", new { code = screen.ScreenCode }, screen);
        }

        // DELETE: api/Screens/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteScreen(string code)
        {
            var screen = await _context.Screens.FindAsync(code);
            if (screen == null)
            {
                return NotFound();
            }

            _context.Screens.Remove(screen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (ScreenExists(code) == false)
                    break;
            }
            return code;
        }

        private bool ScreenExists(string code)
        {
            return _context.Screens.Any(e => e.ScreenCode == code);
        }
    }
}
