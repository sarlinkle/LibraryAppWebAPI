namespace LibraryAppWebAPI.DTOs
{
    public class DisplayBasicBookInfoDTO
    {
        public required string Title { get; set; }    
        public required List<string> AuthorNames { get; set; } = new();
    }
}
