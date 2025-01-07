namespace LibraryAppWebAPI.Models
{
    public class BookDTO
    {
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required string Year { get; set; }
        public required List<Author> Authors { get; set; }
        //public string? Rating { get; set; }
    }
}
