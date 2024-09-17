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
    public class RolesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly RoleMapper _mapper;

        public RolesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new RoleMapper();
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewModel>>> GetRoles()
        {
            return await _context.Roles
                                .Select(model => _mapper.ToViewModel(model))
                                .ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{code}")]
        public async Task<ActionResult<RoleViewModel>> GetRole(string code)
        {
            var role = await _context.Roles
                .Where(model => model.RoleCode == code)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutRole(string code, RoleViewModel role)
        {
            if (code != role.RoleCode)
            {
                return BadRequest();
            }

            Role model = _mapper.ToModel(role);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(code))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> PostRole(RoleViewModel role)
        {
            role.RoleCode = GenerateCode();

            Role model = _mapper.ToModel(role);
            _context.Roles.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.RoleCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRole", new { code = role.RoleCode }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteRole(string code)
        {
            var role = await _context.Roles.FindAsync(code);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (RoleExists(code) == false)
                    break;
            }
            return code;
        }
        private bool RoleExists(string code)
        {
            return _context.Roles.Any(e => e.RoleCode == code);
        }
    }
}
