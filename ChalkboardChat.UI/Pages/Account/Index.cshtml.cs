using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
    public class AccountModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService; // Angelos service

        public string ErrorMessage { get; set; }

        public AccountModel(
            SignInManager<IdentityUser> signInManager,
            IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostDeleteAccount()
        {
            var username = User.Identity.Name;

            var result = await _userService.DeleteAccountAsync(username);

            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage;
                return Page();
            }

            // Logga ut efter att kontot tagits bort
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Login");
        }
    }
}
