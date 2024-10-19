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
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Tools;

namespace Tecnicell.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfosController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly UserInfoMapper _mapper;

        public UserInfosController(TecnicellContext context)
        {
            _context = context;
            _mapper = new UserInfoMapper();
        }

        // GET: api/UserAccounts
        [HttpGet]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<IEnumerable<UserInfoViewModel>>> GetUserInfos()
        {
            return await _context.UserInfos
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/UserAccounts/5
        [HttpGet("{code}")]
        public async Task<ActionResult<UserInfoViewModel>> GetUserInfo(string code)
        {
            var userInfo = await _context.UserInfos
                .Where(model => model.UserCode == code)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (userInfo == null)
            {
                return NotFound();
            }

            return userInfo;
        }

        // PUT: api/UserAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPut("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> PutUserInfo(string code, UserInfoViewModel userInfo)
        {
            if (code != userInfo.UserCode)
            {
                return BadRequest();
            }
            if (userInfo == null) return NotFound();

            UserInfo model = _mapper.ToModel(userInfo);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(code))
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

        // POST: api/UserAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPost]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<ActionResult<UserInfoViewModel>> PostUserInfo(UserInfoViewModel userInfo)
        {
            if (userInfo == null) return NotFound();
            userInfo.UserCode = GenerateCode();

            UserInfo model = _mapper.ToModel(userInfo);
            _context.UserInfos.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserInfoExists(userInfo.UserCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserInfo", new { code = userInfo.UserCode }, userInfo);
        }

        // DELETE: api/UserAccounts/5
        [HttpDelete("{code}")]
        [Authorize(Roles = "KKYW_rkaT_Sñ64_jtRK")]
        public async Task<IActionResult> DeleteUserInfo(string code)
        {
            var userInfo = await _context.UserInfos.FindAsync(code);
            if (userInfo == null)
            {
                return NotFound();
            }

            _context.UserInfos.Remove(userInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (UserInfoExists(code) == false)
                    break;
            }
            return code;
        }

        private bool UserInfoExists(string code)
        {
            return _context.UserInfos.Any(e => e.UserCode == code);
        }
    }
}
