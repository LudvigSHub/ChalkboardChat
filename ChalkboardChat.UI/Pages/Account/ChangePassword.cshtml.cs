using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserService _userService; // Angelos service

        [BindProperty] public string OldPassword { get; set; }
        [BindProperty] public string NewPassword { get; set; }
        [BindProperty] public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }

        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPost()
        {
            if (NewPassword != ConfirmPassword)
            {
                ErrorMessage = "Lösenorden matchar inte.";
                return Page();
            }

            var result = await _userService.ChangePasswordAsync(
                User.Identity.Name,
                OldPassword,
                NewPassword
            );

            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage;
                return Page();
            }

            return RedirectToPage("/Account/Index");
        }
    }

}
