namespace PasteBinClone.Web.Request
{
    public static class Settings
    {
        public static string WebApiBase { get; set; }

        public enum ApiMethod
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}
