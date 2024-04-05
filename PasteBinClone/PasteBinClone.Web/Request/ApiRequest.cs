
using static PasteBinClone.Web.Request.Settings;

namespace PasteBinClone.Web.Request
{
    public class ApiRequest
    {
        public ApiMethod ApiMethod { get; set; } = ApiMethod.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
