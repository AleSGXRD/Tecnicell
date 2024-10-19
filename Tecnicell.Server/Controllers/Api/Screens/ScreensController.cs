using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.AccessoryMappers;
using Tecnicell.Server.Mapper.Classes;
using Tecnicell.Server.Mapper.Classes.ScreenMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.Responses;
using Tecnicell.Server.Models.ViewModel.Screen;
using Microsoft.AspNetCore.Authorization;

namespace Tecnicell.Server.Controllers.Api.Screens
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
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
        public async Task<ActionResult<IEnumerable<ScreenView>>> GetScreens()
        {
            return await _context.ScreenViews.ToListAsync();
        }

        // GET: api/Screens/5
        [HttpGet("{code}")]
        public async Task<ActionResult<ScreenResponse>> GetScreen(string code)
        {
            var element = await _context.Screens
                                .Include(model => model.ImageCodeNavigation)
                                .Where(model => model.ScreenCode == code)
                                .FirstOrDefaultAsync();
            if (element == null)
            {
                return NotFound();
            }
            var View = await _context.ScreenViews
                                    .Where(model => code == model.Code)
                                    .FirstOrDefaultAsync();

            ScreenHistoryMapper historyMapper = new ScreenHistoryMapper();
            var Histories = await _context.ScreenHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Include(model => model.UserCodeNavigation) 
                .Where(model => model.ScreenCode == code)
                .OrderByDescending(model => model.Date)
                .Select(model => historyMapper.ToViewModel(model))
                .ToListAsync();

            ImageMapper imageMapper = new ImageMapper();
            var Image = element.ImageCodeNavigation != null ? imageMapper.ToViewModel(element.ImageCodeNavigation) : null;

            ScreenResponse response = new ScreenResponse
            {
                Histories = Histories,
                View = View,
                Image = Image ?? null
            };

            return response;
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
