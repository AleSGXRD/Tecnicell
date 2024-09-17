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
    public class UserAccountsController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private readonly UserAccountMapper _mapper;

        public UserAccountsController(TecnicellContext context)
        {
            _context = context;
            _mapper = new UserAccountMapper();
        }

        // GET: api/UserAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountViewModel>>> GetUserAccounts()
        {
            return await _context.UserAccounts
                .Include(model => model.RoleNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .ToListAsync();
        }

        // GET: api/UserAccounts/5
        [HttpGet("{code}")]
        public async Task<ActionResult<UserAccountViewModel>> GetUserAccount(string code)
        {
            var userAccount = await _context.UserAccounts
                .Where(model => model.UserCode == code)
                .Include(model => model.RoleNavigation)
                .Select(model => _mapper.ToViewModel(model))
                .FirstOrDefaultAsync();

            if (userAccount == null)
            {
                return NotFound();
            }

            return userAccount;
        }

        // PUT: api/UserAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkcode=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutUserAccount(string code, UserAccountViewModel userAccount)
        {
            if (code != userAccount.UserCode)
            {
                return BadRequest();
            }
            UserAccount model = _mapper.ToModel(userAccount);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(code))
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
        public async Task<ActionResult<UserAccountViewModel>> PostUserAccount(UserAccountViewModel userAccount)
        {
            userAccount.UserCode = GenerateCode();

            UserAccount model = _mapper.ToModel(userAccount);
            _context.UserAccounts.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserAccountExists(userAccount.UserCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserAccount", new { code = userAccount.UserCode }, userAccount);
        }

        // DELETE: api/UserAccounts/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteUserAccount(string code)
        {
            var userAccount = await _context.UserAccounts.FindAsync(code);
            if (userAccount == null)
            {
                return NotFound();
            }

            _context.UserAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateCode()
        {
            string code = "";
            while (true)
            {
                code = GeneratorCode.GenerateCode(4);

                if (UserAccountExists(code) == false)
                    break;
            }
            return code;
        }

        private bool UserAccountExists(string code)
        {
            return _context.UserAccounts.Any(e => e.UserCode == code);
        }
    }
}
