using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
    public class ChangeUsernameModel : PageModel
    {
        private readonly IUserService _userService; // Angelos service

        [BindProperty] public string NewUsername { get; set; }

        public string ErrorMessage { get; set; }

        public ChangeUsernameModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrWhiteSpace(NewUsername))
            {
                ErrorMessage = "Användarnamnet får inte vara tomt.";
                return Page();
            }

            var result = await _userService.ChangeUsernameAsync(
                User.Identity.Name,
                NewUsername
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
