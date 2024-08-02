using PasteBinClone.Identity.Models;

namespace PasteBinClone.Identity.Interfaces
{
    public interface IRecaptchaService
    {
        RecaptchaResponse ValidateCaptcha(string response);
    }
}
