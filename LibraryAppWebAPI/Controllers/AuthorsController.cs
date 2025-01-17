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
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public AuthorsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayAuthorDTO>>> GetAuthors()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .Select(a =>
                new DisplayAuthorDTO() 
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    BookTitles = a.Books.Select(b => b.Title).ToList()

                }).ToListAsync();                                            

            return authors;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayAuthorDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.Id == id)
                .Select(a =>
                new DisplayAuthorDTO()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    BookTitles = a.Books.Select(a => a.Title).ToList()
                }).FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpGet("searchAuthorName/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<DisplayAuthorDTO>>> SearchAuthorName (string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest();
            }

            var authors = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.FirstName.ToLower().Contains(searchTerm) || a.LastName.ToLower().Contains(searchTerm))
                .Select(a =>
                new DisplayAuthorDTO()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    BookTitles = a.Books.Select(a => a.Title).ToList()
                })
                .OrderBy(a => a.LastName)
                .ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        //// PUT: api/Authors/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAuthor(int id, Author author)
        //{
        //    if (id != author.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(author).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AuthorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDTO createAuthorDTO)
        {
            var author = createAuthorDTO.ToAuthor();
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
