using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using PasteBinClone.Identity.Interfaces;
using PasteBinClone.Identity.Models;
using PasteBinClone.Identity.Models.ViewModel;
using PasteBinClone.Identity.Services;
using System.Security.Claims;

namespace PasteBinClone.Identity.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IIdentityServerInteractionService interactionService,
        IRequestService requestService,
        IOptions<RecaptchaOptions> options,
        IRecaptchaService recaptcha) : Controller
    {
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IIdentityServerInteractionService _interactionService = interactionService;
        private readonly IRequestService _requestService = requestService;
        private readonly RecaptchaOptions _options = options.Value;
        private readonly IRecaptchaService _recaptcha = recaptcha;

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                CaptchaKey = _options.Key
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string recapthcaResponse = Request.Form["g-recaptcha-response"].ToString();

            var recapttchaValidate = _recaptcha.ValidateCaptcha(recapthcaResponse);

            if (!recapttchaValidate.IsSuccess)
            {
                ModelState.AddModelError("cap", "Finish captcha!");
                viewModel.CaptchaKey = _options.Key;

                return View(viewModel);
            }

            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found!");
                return View(viewModel);
            }

            //User login 
            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                viewModel.Password, viewModel.RememberMe, false);

            if (result.Succeeded)
            {
                //If the login is successful, create the authentication Options
                var authOptions = new AuthenticationProperties();

                //If the user selected the remember me options, set cookie settings 
                if (viewModel.RememberMe)
                {
                    authOptions.IsPersistent = true;
                    authOptions.ExpiresUtc = DateTimeOffset
                        .UtcNow
                        .Add(TimeSpan.FromDays(30));
                }

                var serverUser = new IdentityServerUser(user.Id);


                await HttpContext.SignInAsync(serverUser, authOptions);

                if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                else
                {
                    return Redirect("https://localhost:44306/Home/Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Error!");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
                CaptchaKey = _options.Key
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string recapthcaResponse = Request.Form["g-recaptcha-response"].ToString();

            var recapttchaValidate = _recaptcha.ValidateCaptcha(recapthcaResponse);

            if (!recapttchaValidate.IsSuccess)
            {
                ModelState.AddModelError("cap", "Finish captcha!");
                viewModel.CaptchaKey = _options.Key;

                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                EmailConfirmed = true,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName
            };

            //Create a new user
            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded && result.Errors.Count() == 0)
            {
                //Ensure that the role exists 
                await EnsureRoleExistsAsync(UserRoles.User);

                //Add role for user to db
                await _userManager.AddToRoleAsync(user, UserRoles.User);

                //Add claims for user to db
                await _userManager.AddClaimsAsync(user, new List<Claim>
                {
                    new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.Role, UserRoles.User)
                });

                //User Login
                var loginResult = await _signInManager.PasswordSignInAsync(user, viewModel.Password,
                    viewModel.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    var serverUser = new IdentityServerUser(user.Id);

                    var authOptions = new AuthenticationProperties();

                    if (viewModel.RememberMe)
                    {
                        authOptions.IsPersistent = true;
                        authOptions.ExpiresUtc = DateTimeOffset
                            .UtcNow
                            .Add(TimeSpan.FromDays(30));
                    }

                    await HttpContext.SignInAsync(serverUser, authOptions);

                    //Send the user to the API
                    await _requestService.SendUser(new ApiUser
                    {
                        UserId = user.Id,
                        Name = user.UserName,
                        Email = user.Email,
                        Role = UserRoles.User
                    });

                    if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                    {
                        return Redirect(viewModel.ReturnUrl);
                    }
                    else
                    {
                        return Redirect("https://localhost:44306/Home/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Register failed.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, 
                    result.Errors.Any() ? string.Join(",", result.Errors.Select(u => u.Description)) : "Register failed.");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (string.IsNullOrEmpty(logoutId))
            {
                return Redirect("https://localhost:44306/Home/Index");
            }

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if(logoutRequest == null)
            {
                return Redirect("https://localhost:44306/Home/Index");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri ?? "https://localhost:44306/Home/Index");
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            var result = await _roleManager.RoleExistsAsync(roleName);

            if (!result)
            {
                var userRole = new IdentityRole()
                {
                    Name = roleName,
                    NormalizedName = roleName
                };
                await _roleManager.CreateAsync(userRole);
            }
        }
    }
}
