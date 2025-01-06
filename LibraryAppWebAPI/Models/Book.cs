namespace LibraryAppWebAPI.Models
{
    public class Book
    {
        public required int BookID { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required string Year { get; set; }
        public required List<Author> Authors { get; set; }
        public string? Rating { get; set; }
        public required bool Available { get; set; }
        public DateOnly? BorrowedDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

    }
}
