using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public static class DTOExtensions
    {
        public static Book ToBook(this CreateBookDTO createBookDTO)
        {
            return new Book
            {
                Title = createBookDTO.Title,
                ISBN = createBookDTO.ISBN,
                ReleaseDate = new DateOnly(createBookDTO.ReleaseYear, 1, 1),
                Authors = createBookDTO.Authors,
            };
        }
        public static BookDTO ToBookDTO(this Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                ReleaseYear = book.ReleaseDate.Year,
                Authors = book.Authors,
            };
        }
        //public static LibraryUser ToLibraryUser(this CreateLibraryUserDTO createLibraryUserDTO)
        //{
        //    return new LibraryUser
        //    {
        //        FirstName = createLibraryUserDTO.FirstName,
        //        LastName = createLibraryUserDTO.LastName,
        //    };
        //}
        public static LibraryUserDTO ToLibraryUserDTO(this LibraryUser libraryUser)
        {
            return new LibraryUserDTO
            {
                LibraryCardNumber = libraryUser.LibraryCardNumber,
                FirstName = libraryUser.FirstName,
                LastName = libraryUser.LastName
            };
        }
    }
}
