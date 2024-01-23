using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Microservice.EndPoint.Pages
{
    public class AuthenticationModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnGetLogout()
        {
            //signout in identity service and redirect there again
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //SignOutAsync is extention method by identityserver
            //signout there
            HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
