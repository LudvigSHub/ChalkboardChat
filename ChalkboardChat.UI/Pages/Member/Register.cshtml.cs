namespace ChalkboardChat.UI.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty, Required]
        public string Username { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        [BindProperty, Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _userService.RegisterAsync(Username, Password);

            if (result.Success)
                return RedirectToPage("/Index");

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return Page();
        }
    }
}