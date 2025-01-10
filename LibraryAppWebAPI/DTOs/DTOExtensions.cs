using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public static class DTOExtensions
    {

        public static void SomeHTnig()
        {
            CreateBookDTO a = new CreateBookDTO();
            a.ToBook()
        }
        public static Book ToBook(this CreateBookDTO createBookDTO)
        {
            return new Book
            {
                Title = createBookDTO.Title,
                ISBN = createBookDTO.ISBN,
                ReleaseDate = new DateOnly(createBookDTO.ReleaseYear, 1, 1),
                AuthorIds = createBookDTO.AuthorIds,
                Rating = createBookDTO.Rating,
            };
        }
        public static Book ToBook(this DisplayBookDTO displayBookDTO)
        {
            return new Book
            {
                Title = displayBookDTO.Title,
                ISBN = displayBookDTO.ISBN,
                ReleaseDate = displayBookDTO.ReleaseDate,
                AuthorIds = displayBookDTO.AuthorIds.ToList(), //Get author names
                Rating = displayBookDTO.Rating,
            };
        }

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
        //public static LibraryUser ToLibraryUser(this CreateLibraryUserDTO createLibraryUserDTO)
        //{
        //    return new LibraryUser
        //    {
        //        FirstName = createLibraryUserDTO.FirstName,
        //        LastName = createLibraryUserDTO.LastName,
        //    };
        //}
        public static LibraryUser ToLibraryUser(this CreateNewLibraryUserDTO newLibraryUserDTO)
        {
            return new LibraryUser
            {
                FirstName = newLibraryUserDTO.FirstName,
                LastName = newLibraryUserDTO.LastName
            };
        }
        public static LibraryUser ToLibraryUser(this DisplayLibraryUserDTO displayLibraryUserDTO)
        {
            return new LibraryUserDTO
            {
                LibraryCardNumber = displayLibraryUserDTO.LibraryCardNumber,
                FirstName = displayLibraryUserDTO.FirstName,
                LastName = displayLibraryUserDTO.LastName
            };
        }
    }
}
