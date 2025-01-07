namespace LibraryAppWebAPI.Models
{
    public class Book
    {
        public required int BookID { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required string Year { get; set; }
        public required List<Author> Authors { get; set; }

        //public required int ShelfID { get; set; }
        //public string? Rating { get; set; }
    }
}
