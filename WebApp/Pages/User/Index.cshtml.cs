using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.User
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                HttpContext.Response.Redirect("/Login");
            }
        }
    }
}
