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
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayDetailedBookInfoDTO>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Select(b =>
            new DisplayDetailedBookInfoDTO()
            { 
                Title = b.Title,
                ISBN = b.ISBN,
                AuthorNames = b.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
                ReleaseDate = b.ReleaseDate,
            }).ToListAsync();

             return books;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayDetailedBookInfoDTO>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Where(b => b.Id == id)
                .Select(b =>
            new DisplayDetailedBookInfoDTO()
            {
                Title = b.Title,
                ISBN = b.ISBN,
                AuthorNames = b.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
                ReleaseDate = b.ReleaseDate,
            }).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // GET: api/Books/searchTitle/searchTerm
        [HttpGet("searchBookTitle/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<DisplayDetailedBookInfoDTO>>> SearchBookTitle(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest();
            }

            var books = await _context.Books
                .Include(b => b.Authors)
                .Where(b => b.Title.ToLower().Contains(searchTerm))
                .Select(b =>
                new DisplayDetailedBookInfoDTO()
                {
                    Title = b.Title,
                    ISBN = b.ISBN,
                    AuthorNames = b.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
                    ReleaseDate = b.ReleaseDate,
                }).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }
            return books;
        }

        // GET: api/Books/searchISBN/searchTerm
        [HttpGet("searchISBN/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<DisplayDetailedBookInfoDTO>>> SearchBookISBN(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest();
            }

            var books = await _context.Books.Search(searchTerm).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }
            return books;
        }

        //// PUT: api/Books/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
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

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(CreateBookDTO createBookDTO)
        {
            //TO do - get the authors
            var book = createBookDTO.ToBook(new List<Author>());

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var response = CreatedAtAction("GetBook", new { id = book.Id }, book);
            return response;
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
