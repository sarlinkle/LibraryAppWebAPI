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
    public class CheckoutsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public CheckoutsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Checkouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayCheckoutDTO>>> GetCheckouts()
        {
            var checkouts = await _context.Checkouts
                .Include(c => c.Books)
                .Include(c => c.LibraryUser)
                .Select(c =>
                new DisplayCheckoutDTO()
                {
                    LibraryCardNumber = c.LibraryUser.LibraryCardNumber,
                    BookTitles = c.Books.Select(c => c.Title).ToList(),
                    DateBorrowed = c.DateBorrowed,
                    DateDue = c.DateDue
                }).ToListAsync();

            return checkouts;
        }

        // GET: api/Checkouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisplayCheckoutDTO>> GetCheckout(int id)
        {
            var checkout = await _context.Checkouts
                .Include(c => c.Books)
                .Include(c => c.LibraryUser)
                .Where(c => c.Id == id)
                .Select(c =>
                new DisplayCheckoutDTO()
                {
                    LibraryCardNumber = c.LibraryUser.LibraryCardNumber,
                    DateBorrowed = c.DateBorrowed,
                    DateDue = c.DateDue,
                    BookTitles = c.Books.Select(c => c.Title).ToList(),
                }).FirstOrDefaultAsync();

            if (checkout == null)
            {
                return NotFound();
            }

            return checkout;
        }

        //// PUT: api/Checkouts/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCheckout(int id, Checkout checkout)
        //{
        //    if (id != checkout.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(checkout).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CheckoutExists(id))
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

        // POST: api/Checkouts/borrow
        [HttpPost("Borrow")]
        public async Task<ActionResult<Checkout>> CreateCheckout(CreateCheckoutDTO createCheckoutDTO)
        {
            //var books = await _context.Books
            //    .Select(b => b.Id).ToListAsync();

            var books = await _context.Books
                .Select(b =>
                new Book()
                {
                    Id = 0,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Authors = b.Authors,
                    ReleaseDate = b.ReleaseDate
                }).ToListAsync();

            if (books == null)
            {
                return NotFound("Book not found");
            }

            var libraryUser = await _context.LibraryUsers.FindAsync(createCheckoutDTO.LibraryUserId);
            if (libraryUser == null)
            {
                return NotFound("Library user not found");
            }

            var checkout = new Checkout()
            {
                Id = 0,
                LibraryUser = libraryUser,
                Books = books,
                DateBorrowed = DateTime.Now,
                DateDue = DateTime.Now.AddDays(30),
            };
            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckout", new { id = checkout.Id }, checkout);
        }

        // PUT: api/Checkouts/return/5
        [HttpPut("Return/{id}")]
        public async Task<IActionResult> CreateReturn(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout == null) 
            { 
                return NotFound("Checkout not found"); 
            }

            checkout.DateReturned = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckoutExists(id))
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

        // DELETE: api/Checkouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckout(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }

            _context.Checkouts.Remove(checkout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkouts.Any(e => e.Id == id);
        }
    }
}
