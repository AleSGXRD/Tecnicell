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
using Tecnicell.Server.Mapper.Classes.WorkDiaryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Models.ViewModel.DiaryWork;

namespace Tecnicell.Server.Controllers.Api.DiaryWorks
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryWorksController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly DiaryWorksMappers _mapper;

        public DiaryWorksController(TecnicellContext context)
        {
            _context = context;
            _mapper = new DiaryWorksMappers();
        }

        // GET: api/DiaryWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryWorkViewModel>>> GetDiaryWorks()
        {
            return await _context.DiaryWorks
                                .Include(model => model.UserCodeNavigation)
                                .Include(model => model.SaleCodeNavigation)
                                .OrderDescending()
                                .Select(model => _mapper.ToViewModel(model))
                                .ToListAsync();
        }

        // GET: api/DiaryWorks/5
        [HttpGet("{date}")]
        public async Task<ActionResult<DiaryWorkViewModel>> GetDiaryWork(DateTime date)
        {
            var DiaryWork = await _context.DiaryWorks
                                .Where(model => model.Date == date)
                                .Select(model => _mapper.ToViewModel(model))
                                .FirstOrDefaultAsync();

            if (DiaryWork == null)
            {
                return NotFound();
            }

            return DiaryWork;
        }

        // PUT: api/DiaryWorks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{date}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
        public async Task<IActionResult> PutDiaryWork(DateTime date, DiaryWorkViewModel viewmodel)
        {
            if (date != viewmodel.Date)
            {
                return BadRequest();
            }
            DiaryWork model = _mapper.ToModel(viewmodel);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryWorkExists(date))
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

        // POST: api/DiaryWorks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
        public async Task<ActionResult<DiaryWorkViewModel>> PostDiaryWork(DiaryWorkViewModel viewmodel)
        {
            DiaryWork model = _mapper.ToModel(viewmodel);
            _context.DiaryWorks.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiaryWorkExists(viewmodel.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiaryWork", new { code = viewmodel.Date }, viewmodel);
        }

        // DELETE: api/DiaryWorks/5
        [HttpDelete("{date}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
        public async Task<IActionResult> DeleteDiaryWork(DateTime date)
        {
            var model = await _context.DiaryWorks.FindAsync(date);
            if (model == null)
            {
                return NotFound();
            }

            _context.DiaryWorks.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool DiaryWorkExists(DateTime date)
        {
            return _context.DiaryWorks.Any(e => e.Date == date);
        }
    }
}
