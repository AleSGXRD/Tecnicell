using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.AccessoryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Controllers.Api.Accessories
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
    public class AccessoryTypesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly AccessoryTypeMapper _mapper;

        public AccessoryTypesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new AccessoryTypeMapper();
        }

        // GET: api/AccessoryTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessoryTypeViewModel>>> GetAccessoryTypes()
        {
            return await _context.AccessoryTypes
                .Select(acType => _mapper.ToViewModel(acType))
                .ToListAsync();
        }

        // GET: api/AccessoryTypes/5
        [HttpGet("{code}")]
        public async Task<ActionResult<AccessoryTypeViewModel>> GetAccessoryType(string code)
        {
            var accessoryType = await _context.AccessoryTypes.FindAsync(code);

            if (accessoryType == null)
            {
                return NotFound();
            }

            return _mapper.ToViewModel(accessoryType);
        }

        // PUT: api/AccessoryTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutAccessoryType(string code, AccessoryTypeViewModel accessoryType)
        {
            if (code != accessoryType.AccessoryTypeCode)
            {
                return BadRequest();
            }

            AccessoryType acType = _mapper.ToModel(accessoryType);

            _context.Entry(acType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoryTypeExists(code))
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

        // POST: api/AccessoryTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessoryTypeViewModel>> PostAccessoryType(AccessoryTypeViewModel accessoryType)
        {
            accessoryType.AccessoryTypeCode = GenerateCode();

            AccessoryType acType = _mapper.ToModel(accessoryType);

            _context.AccessoryTypes.Add(acType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccessoryTypeExists(accessoryType.AccessoryTypeCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccessoryType", new { code = accessoryType.AccessoryTypeCode }, accessoryType);
        }

        // DELETE: api/AccessoryTypes/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteAccessoryType(string code)
        {
            var accessoryType = await _context.AccessoryTypes.FindAsync(code);
            if (accessoryType == null)
            {
                return NotFound();
            }

            _context.AccessoryTypes.Remove(accessoryType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (AccessoryTypeExists(code) == false)
                    break;
            }
            return code;
        }


        private bool AccessoryTypeExists(string code)
        {
            return _context.AccessoryTypes.Any(e => e.AccessoryTypeCode == code);
        }
    }
}
