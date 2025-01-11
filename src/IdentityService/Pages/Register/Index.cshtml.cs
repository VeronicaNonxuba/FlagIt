using System.Security.Claims;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public Index(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel? Input { get; set; }

        [BindProperty]
        public bool RegisterSuccess { get; set; }
        public IActionResult OnGet(string? returnUrl)
        {
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input!.Button != "register") return Redirect("~/");


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input!.Username,
                    Email = Input.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password ?? throw new ArgumentNullException(nameof(Input.Password)));
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user,
                                new Claim(JwtClaimTypes.Name, Input.FullName ??
                                        throw new ArgumentNullException(nameof(Input.FullName))));
                    RegisterSuccess = true;
                    return Page();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
