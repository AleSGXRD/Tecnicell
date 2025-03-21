﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Context;
using Tecnicell.Server.Mapper.Classes;
using Tecnicell.Server.Mapper.Classes.BatteryMappers;
using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.Responses;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Controllers.Api.Batteries
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK, YHYc_ISif_7os0_ZqBR")]
    public class BatteriesController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly BatteryMapper _mapper;

        public BatteriesController(TecnicellContext context)
        {
            _context = context;
            _mapper = new BatteryMapper();
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatteryView>>> GetBatteries()
        {
            return await _context.BatteryViews.ToListAsync();
        }

        // GET: api/Batteries/5
        [HttpGet("{code}")]
        public async Task<ActionResult<BatteryResponse>> GetBattery(string code)
        {
            var element = await _context.Batteries
                                .Include(model => model.ImageCodeNavigation)
                                .Where(model => model.BatteryCode == code)
                                .FirstOrDefaultAsync();
            if (element == null)
            {
                return NotFound();
            }
            var View = await _context.BatteryViews
                                    .Where(model => code == model.Code)
                                    .FirstOrDefaultAsync();

            BatteryHistoryMapper historyMapper = new BatteryHistoryMapper();
            var Histories = await _context.BatteryHistories
                .Include(model => model.ActionHistoryNavigation)
                .Include(model => model.SaleCodeNavigation)
                .Include(model => model.ToBranchNavigation)
                .Include(model => model.UserCodeNavigation)
                .Where(model => model.BatteryCode == code)
                .OrderByDescending(model => model.Date)
                .Select(model => historyMapper.ToViewModel(model))
                .ToListAsync();

            ImageMapper imageMapper = new ImageMapper();
            var Image = element.ImageCodeNavigation != null ? imageMapper.ToViewModel(element.ImageCodeNavigation) : null;

            BatteryResponse response = new BatteryResponse
            {
                Histories = Histories,
                View = View,
                Image = Image ?? null
            };

            return response;
        }

        // PUT: api/Batteries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutBattery(string code, BatteryViewModel battery)
        {
            if (code != battery.BatteryCode)
            {
                return BadRequest();
            }

            Battery model = _mapper.ToModel(battery);

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryExists(code))
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

        // POST: api/Batteries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BatteryViewModel>> PostBattery(BatteryViewModel battery)
        {
            battery.BatteryCode = GenerateCode();

            Battery model = _mapper.ToModel(battery);
            _context.Batteries.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BatteryExists(battery.BatteryCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBattery", new { code = battery.BatteryCode }, battery);
        }

        // DELETE: api/Batteries/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteBattery(string code)
        {
            var battery = await _context.Batteries.FindAsync(code);
            if (battery == null)
            {
                return NotFound();
            }

            _context.Batteries.Remove(battery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (BatteryExists(code) == false)
                    break;
            }
            return code;
        }
        private bool BatteryExists(string code)
        {
            return _context.Batteries.Any(e => e.BatteryCode == code);
        }
    }
}
