namespace LibraryAppWebAPI.Models
{
    public class LibraryUser
    {
        public required int LibraryUserID { get; set; }
        public required string LibraryCardNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
