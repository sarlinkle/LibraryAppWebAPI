namespace LibraryAppWebAPI.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public required Book Book { get; set; }
        public required LibraryUser LibraryUser { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }       
    }
}
