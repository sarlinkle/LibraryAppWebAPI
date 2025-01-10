using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAppWebAPI.Data;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.DTOs;

namespace LibraryAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayBookDTO>>> GetBooks()
        {
            IEnumerable<Book> books = await _context.Books.ToListAsync();
            var bookDTOs = books.Select(book => new DisplayBookDTO() { Title = book.Title, ReleaseDate = new DateOnly(), ISBN = book.ISBN, AuthorIds = [] }).ToList();
            return bookDTOs;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayBookDTO>> GetBook(int id)
        {
            var book = await _context.Books.ToListAsync();
            var bookDTO = book.Select(book => new DisplayBookDTO() { Title = book.Title, ReleaseDate = new DateOnly(), ISBN = book.ISBN, AuthorIds = [] }).Where(book.Id == id);

            if (bookDTO == null)
            {
                return NotFound();
            }

            return bookDTO;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, CreateReturnDTO book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(CreateBookDTO createBookDTO)
        {
            var book = createBookDTO.ToBook();

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
