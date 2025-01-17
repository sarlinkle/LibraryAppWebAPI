﻿using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.DTOs
{
    public class CreateBookDTO
    {
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required int ReleaseYear { get; set; }
        public required List<int> AuthorIds { get; set; } = new();
        public string? Rating { get; set; }
    }
}
