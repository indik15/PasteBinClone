using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using PasteBinClone.Web.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<IBaseService, BaseService>();

Settings.WebApiBase = builder.Configuration["WebApi:Url"];

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", options =>
    {
        options.Cookie.HttpOnly = true;
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        options.AccessDeniedPath = "/lll/";
        options.SlidingExpiration = true;

        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = async context =>
            {
                var expiresAt = context.Properties.GetTokenValue("expires_at");

                if (!string.IsNullOrEmpty(expiresAt) &&
                DateTimeOffset.TryParse(expiresAt, out var expiresDateTime))
                {
                    if (expiresDateTime <= DateTimeOffset.UtcNow)
                    {
                        context.RejectPrincipal();
                        await context.HttpContext.SignOutAsync("Cookies");
                    }
                }
            }
        };
    })

    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["WebApi:IdentityApi"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "PasteBinCloneAPi";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        //options.UseTokenLifetime = true;

        options.Scope.Add("PasteBinCloneAPi");
        options.Scope.Add("offline_access");
        options.SaveTokens = true;
        options.ClaimActions.MapJsonKey("role", "role");

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
