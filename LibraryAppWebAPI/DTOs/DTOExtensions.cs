using LibraryAppWebAPI.Controllers;
using LibraryAppWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryAppWebAPI.DTOs
{
    public static class DTOExtensions
    {
        public static Author ToAuthor(this CreateAuthorDTO createAuthorDTO)
        {
            var author = new Author
            {
                Id = 0,
                FirstName = createAuthorDTO.FirstName,
                LastName = createAuthorDTO.LastName,
                Books = new()
            };
            return author;
        }
        public static LibraryUser ToLibraryUser(this CreateNewLibraryUserDTO createNewLibraryUserDTO)
        {
            Random random = new Random();

            var libraryUser = new LibraryUser()
            {
                FirstName = createNewLibraryUserDTO.FirstName,
                LastName = createNewLibraryUserDTO.LastName,
                LibraryCardNumber = random.Next(1000000, 9999999)
            };

            return libraryUser;
        }

        public static Checkout ToCheckout(this CreateCheckoutDTO createCheckoutDTO, List<Book> books, LibraryUser libraryUser)
        {
            var checkout = new Checkout()
            {
                Id = 0,
                Books = books,
                LibraryUser = libraryUser,
                DateBorrowed = DateTime.Now,
                DateDue = DateTime.Now.AddDays(30)
            };

            return checkout;
        }

        public static Checkout ToCheckout(this DisplayCheckoutDTO displayCheckoutDTO, List<Book> books, LibraryUser libraryUser)
        {
            var checkout = new Checkout()
            {
                Id = 0,
                Books = books,
                LibraryUser = libraryUser,
                DateBorrowed = displayCheckoutDTO.DateBorrowed,
                DateDue = displayCheckoutDTO.DateDue
            };

            return checkout;
        }

        //public static IQueryable<DisplayCheckoutDTO> Filter(this IQueryable<Checkout> query)
        //{
        //    return query.Include(c => c.Books)
        //   .Include(c => c.LibraryUser)
        //   .Select(c => c.ToCheckout(c.Books, c.LibraryUser));
        //}

        public static DisplayDetailedBookInfoDTO ToDetailedBook(this Book book)
        {
            var detailedBook = new DisplayDetailedBookInfoDTO()
            {
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorNames = book.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
                ReleaseYear = book.ReleaseDate.Year,
            };

            return detailedBook;
        }

        public static IQueryable<DisplayDetailedBookInfoDTO> Search(this IQueryable<Book> query, string searchTerm)
        {
            return query.Include(b => b.Authors)
               .Where(b => b.ISBN.Contains(searchTerm))
               .Select(b => b.ToDetailedBook());
        }
        public static Book ToBook(this CreateBookDTO createBookDTO, List<Author> authors)
        {
            //_context.Authors.Where(a => a.Id.Equals(createBookDTO.AuthorIds)).ToList()
            return new Book
            {
                Id = 0,
                Title = createBookDTO.Title,
                ISBN = createBookDTO.ISBN,
                ReleaseDate = new DateOnly(createBookDTO.ReleaseYear, 1, 1),
                Authors = authors

            };
        }
        //public static Book ToBook(this DisplayBookDTO displayBookDTO)
        //{
        //    return new Book
        //    {
        //        Title = displayBookDTO.Title,
        //        ISBN = displayBookDTO.ISBN,
        //        ReleaseDate = displayBookDTO.ReleaseDate,
        //        Authors = displayBookDTO.AuthorNames.ToList(), //Get author names
        //        Rating = displayBookDTO.Rating,
        //    };
        //}

        //public static Book ToBook(this CreateCheckoutDTO createCheckoutDTO)
        //{
        //    return new Book
        //    {
        //        Id = borrowBookDTO.Id
        //    };
        //}
        //public static CreateCheckoutDTO ToBookDTO(this Book book)
        //{
        //    return new CreateCheckoutDTO
        //    {
        //        Id = book.Id,
        //        Title = book.Title,
        //        ISBN = book.ISBN,
        //        ReleaseYear = book.ReleaseDate.Year,
        //        AuthorIds = book.AuthorIds,
        //    };
        //}
        //public static LibraryUser ToLibraryUser(this CreateNewLibraryUserDTO createNewLibraryUserDTO)
        //{
        //    return new LibraryUser
        //    {
        //        FirstName = createNewLibraryUserDTO.FirstName,
        //        LastName = createNewLibraryUserDTO.LastName
        //    };
        //}
        //public static LibraryUser ToLibraryUser(this DisplayLibraryUserDTO displayLibraryUserDTO)
        //{
        //    return new LibraryUser
        //    {
        //        LibraryCardNumber = displayLibraryUserDTO.LibraryCardNumber,
        //        FirstName = displayLibraryUserDTO.FirstName,
        //        LastName = displayLibraryUserDTO.LastName
        //    };
        //}
    }
}
