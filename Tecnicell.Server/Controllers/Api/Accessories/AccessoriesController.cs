using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes;
using Tecnicell.Server.Mapper.Classes.AccessoryMappers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.Responses;
using Tecnicell.Server.Models.ViewModel.Accessory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tecnicell.Server.Controllers.Api.Accessories
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
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
        public async Task<ActionResult<IEnumerable<AccessoryView>>> Get()
        {
            return await _context.AccessoryViews.ToListAsync();
        }

        // GET api/<AccessoriesController>/5
        [HttpGet("{code}")]
        public async Task<ActionResult<AccessoryResponse>> Get(string code)
        {
            var element = await _context.Accessories
                                .Include(model => model.ImageCodeNavigation)
                                .Where(model => model.AccessoryCode == code)
                                .FirstOrDefaultAsync();
            if (element == null)
            {
                return NotFound();
            }
            var View = await _context.AccessoryViews
                                    .Where(model => code == model.Code)
                                    .FirstOrDefaultAsync();

            AccessoryHistoryMapper historyMapper = new AccessoryHistoryMapper();
            var Histories = await _context.AccessoryHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Include(model => model.UserCodeNavigation)
                .Where(model => model.AccessoryCode == code)
                .OrderByDescending(model => model.Date)
                .Select(model => historyMapper.ToViewModel(model))
                .ToListAsync();

            ImageMapper imageMapper = new ImageMapper();
            var Image = element.ImageCodeNavigation != null ? imageMapper.ToViewModel(element.ImageCodeNavigation) : null;

            AccessoryResponse response = new AccessoryResponse
            {
                Histories = Histories,
                View = View,
                Image = Image ?? null
            };

            return response;
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
        public async Task<IActionResult> Delete(string code)
        {
            var cliente = await _context.Accessories.FindAsync(code);
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
