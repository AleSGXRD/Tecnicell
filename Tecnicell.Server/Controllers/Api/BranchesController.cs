using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class BranchesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly BranchMapper _mapper;

        public BranchesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new BranchMapper();
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchViewModel>>> GetBranches()
        {
            return await _context.Branches
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/Branches/5
        [HttpGet("{code}")]
        public async Task<ActionResult<BranchViewModel>> GetBranch(string code)
        {
            var branch = await _context.Branches
                .Where(model => model.BranchCode == code)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutBranch(string code, BranchViewModel branch)
        {
            if (code != branch.BranchCode)
            {
                return BadRequest();
            }

            Branch model = _mapper.ToModel(branch);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(code))
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

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BranchViewModel>> PostBranch(BranchViewModel branch)
        {
            branch.BranchCode = GenerateCode();

            Branch model = _mapper.ToModel(branch);
            _context.Branches.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BranchExists(branch.BranchCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBranch", new { code = branch.BranchCode }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteBranch(string code)
        {
            var branch = await _context.Branches.FindAsync(code);
            if (branch == null)
            {
                return NotFound();
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (BranchExists(code) == false)
                    break;
            }
            return code;
        }
        private bool BranchExists(string code)
        {
            return _context.Branches.Any(e => e.BranchCode == code);
        }
    }
}
