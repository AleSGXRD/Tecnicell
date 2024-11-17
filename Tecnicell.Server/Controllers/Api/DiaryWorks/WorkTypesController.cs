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
    public class WorkTypesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly WorkTypeMappers _mapper;

        public WorkTypesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new WorkTypeMappers();
        }

        // GET: api/WorkTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTypeViewModel>>> GetWorkTypes()
        {
            return await _context.WorkTypes
                                .Select(model => _mapper.ToViewModel(model))
                                .ToListAsync();
        }

        // GET: api/WorkTypes/5
        [HttpGet("{code}")]
        public async Task<ActionResult<WorkTypeViewModel>> GetWorkType(string code)
        {
            var WorkType = await _context.WorkTypes
                                .Where(model => model.Name == code)
                                .Select(model => _mapper.ToViewModel(model))
                                .FirstOrDefaultAsync();

            if (WorkType == null)
            {
                return NotFound();
            }

            return WorkType;
        }

        // PUT: api/WorkTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> PutWorkType(string code, WorkTypeViewModel viewmodel)
        {
            if (code != viewmodel.Name)
            {
                return BadRequest();
            }
            WorkType model = _mapper.ToModel(viewmodel);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTypeExists(code))
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

        // POST: api/WorkTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<WorkTypeViewModel>> PostWorkType(WorkTypeViewModel viewmodel)
        {
            WorkType model = _mapper.ToModel(viewmodel);
            _context.WorkTypes.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkTypeExists(viewmodel.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWorkType", new { code = viewmodel.Name }, viewmodel);
        }

        // DELETE: api/WorkTypes/5
        [HttpDelete("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> DeleteWorkType(string code)
        {
            var model = await _context.WorkTypes.FindAsync(code);
            if (model == null)
            {
                return NotFound();
            }

            _context.WorkTypes.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool WorkTypeExists(string code)
        {
            return _context.WorkTypes.Any(e => e.Name == code);
        }
    }
}
