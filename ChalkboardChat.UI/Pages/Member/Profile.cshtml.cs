using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string NewUsername { get; set; }

        [BindProperty]
        public string OldPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }   // <-- DU SAKNADE DENNA

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostChangeUsernameAsync()
        {
            var userId = _userService.GetCurrentUserId();

            var result = await _userService.ChangeUsernameAsync(userId, NewUsername);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            if (NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Lösenorden matchar inte.");
                return Page();
            }

            var userId = _userService.GetCurrentUserId();

            var result = await _userService.ChangePasswordAsync(userId, OldPassword, NewPassword);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync()
        {
            var userId = _userService.GetCurrentUserId();

            var result = await _userService.DeleteAccountAsync(userId);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return Page();
            }

            await _userService.LogoutAsync();

            return RedirectToPage("/Index");
        }
    }
}