namespace PasteBinClone.API.Response
{
    public class ResponseAPI
    {
        public bool IsSuccess { get; set; } = true;
        public object Data {  get; set; }
        public List<string> Errors { get; set; } = [];
    }
}
