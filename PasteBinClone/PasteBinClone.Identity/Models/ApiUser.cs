namespace PasteBinClone.Identity.Models
{
    //Class to send the user to the API
    public class ApiUser
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

    }
}
