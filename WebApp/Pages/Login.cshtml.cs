using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _signInManager.PasswordSignInAsync(Username, Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(Username);
                var roles = await _signInManager.UserManager.GetRolesAsync(user);

                HttpContext.Session.SetString("UserRole", roles.FirstOrDefault());

                if (roles.Contains("Admin"))
                {
                    return RedirectToPage("/Admin/Index.cshtml");
                }
                else if (roles.Contains("User"))
                {
                    return RedirectToPage("/User/Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
