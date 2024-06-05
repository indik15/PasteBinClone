namespace PasteBinClone.Identity.Models
{
    //Class to send the user to the API
    public class ApiUser
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
