namespace PasteBinClone.Web.Interfaces
{
    public interface IUserInfo
    {
        string GetUserId(string accessToken);
    }
}
