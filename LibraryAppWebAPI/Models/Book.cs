﻿namespace LibraryAppWebAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public required List<Author> Authors { get; set; } = new();
        public string? Rating { get; set; }
    }
}
