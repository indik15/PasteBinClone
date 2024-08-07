namespace PasteBinClone.Web.Models.ViewModel.Paste
{
    public class UserPasteInfoVM
    {
        public Guid Id { get; set; }
        public int TotalPastes { get; set; }
        public int TotalPublicPastes { get; set; }
        public int TotalPrivatePastes { get; set; }
        public int TotalActivePastes { get; set; }

        public string UserId { get; set; }
        public ApiUserVM ApiUser { get; set; }
    }
}
