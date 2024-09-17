using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes.AccessoryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Accessory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tecnicell.Server.Controllers.Api.Accessories
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly AccessoryMapper _mapper;

        public AccessoriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new AccessoryMapper();
        }

        // GET: api/<AccessoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessoryViewModel>>> Get()
        {
            return await _context.Accessories
                .Include(ac => ac.AccessoryHistories)
                .Include(ac => ac.AccessoryTypeNavigation)
                .Select(ac => _mapper.ToViewModel(ac))
                .ToListAsync();
        }

        // GET api/<AccessoriesController>/5
        [HttpGet("{code}")]
        public async Task<ActionResult<AccessoryViewModel>> Get(string code)
        {
            var accessory = await _context.Accessories
                .Where(ac => ac.AccessoryCode == code)
                .Include(ac => ac.AccessoryHistories)
                .Include(ac => ac.AccessoryTypeNavigation)
                .Select(ac => _mapper.ToViewModel(ac))
                .FirstOrDefaultAsync();

            if (accessory == null)
                return NotFound();

            return accessory;
        }

        // POST api/<AccessoriesController>
        [HttpPost]
        public async Task<ActionResult<AccessoryViewModel>> Post(AccessoryViewModel accessory)
        {
            accessory.AccessoryCode = GenerateCode();

            Accessory model = _mapper.ToModel(accessory);

            _context.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccessoryExists(accessory.AccessoryCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Get", new { code = accessory.AccessoryCode }, accessory);
        }

        // PUT api/<AccessoriesController>/5
        [HttpPut("{code}")]
        public async Task<IActionResult> Put(string code, AccessoryViewModel value)
        {
            if (code != value.AccessoryCode)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.ToModel(value)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoryExists(code))
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

        // DELETE api/<AccessoriesController>/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string id)
        {
            var cliente = await _context.Accessories.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Accessories.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (AccessoryExists(code) == false)
                    break;
            }
            return code;
        }

        private bool AccessoryExists(string code)
        {
            return _context.Accessories.Any(ac => ac.AccessoryCode == code);
        }
    }
}
