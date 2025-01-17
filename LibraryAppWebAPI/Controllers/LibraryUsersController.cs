using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.DTOs;

namespace LibraryAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryUsersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LibraryUsersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/LibraryUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayLibraryUserDTO>>> GetLibraryUsers()
        {
            var libraryUsers = await _context.LibraryUsers
                .Select(l => 
                new DisplayLibraryUserDTO()
                { 
                    FullName = $"{l.FirstName} {l.LastName}",
                    LibraryCardNumber = l.LibraryCardNumber,                   
                }).ToListAsync();

            return libraryUsers;
        }

        // GET: api/LibraryUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayLibraryUserDTO>> GetLibraryUser(int id)
        {
            var libraryUser = await _context.LibraryUsers
                .Where(l => l.Id == id)
                .Select(l =>
                new DisplayLibraryUserDTO()
                {
                    FullName = $"{l.FirstName} {l.LastName}",
                    LibraryCardNumber = l.LibraryCardNumber,
                }).FirstOrDefaultAsync();

            if (libraryUser == null)
            {
                return NotFound();
            }

            return libraryUser;
        }

        // PUT: api/LibraryUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryUser(int id, LibraryUser libraryUser)
        {
            if (id != libraryUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(libraryUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryUserExists(id))
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

        // POST: api/LibraryUsers
        [HttpPost]
        public async Task<ActionResult<LibraryUser>> PostLibraryUser(CreateNewLibraryUserDTO createNewLibraryUserDTO)
        {
            var libraryUser = createNewLibraryUserDTO.ToLibraryUser();
            _context.LibraryUsers.Add(libraryUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibraryUser", new { id = libraryUser.Id }, libraryUser);
        }

        // DELETE: api/LibraryUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryUser(int id)
        {
            var libraryUser = await _context.LibraryUsers.FindAsync(id);
            if (libraryUser == null)
            {
                return NotFound();
            }

            _context.LibraryUsers.Remove(libraryUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibraryUserExists(int id)
        {
            return _context.LibraryUsers.Any(e => e.Id == id);
        }
    }
}
