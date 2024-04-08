namespace PasteBinClone.Web.Models
{
    public class ResponseAPI<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T Data { get; set; }
    }
}
