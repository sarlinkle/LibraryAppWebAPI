namespace LibraryAppWebAPI.DTOs
{
    public class CreateAuthorDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        
        //public List<int>? BookIds { get; set; } = new();
    }
}
