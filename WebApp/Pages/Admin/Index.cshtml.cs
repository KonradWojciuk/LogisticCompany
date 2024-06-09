using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                HttpContext.Response.Redirect("/Login");
            }
        }
    }
}
