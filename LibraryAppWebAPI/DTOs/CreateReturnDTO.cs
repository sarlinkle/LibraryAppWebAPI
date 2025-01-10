﻿namespace LibraryAppWebAPI.DTOs
{
    public class CreateReturnDTO
    {
        public required int CheckoutId { get; set; }
        public string? Rating { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}