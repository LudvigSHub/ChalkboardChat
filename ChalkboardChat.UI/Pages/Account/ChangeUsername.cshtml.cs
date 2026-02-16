using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ChangeUsernameModel : PageModel
{
    private readonly IUserService _userService;
    private readonly UserManager<IdentityUser> _userManager;

    public ChangeUsernameModel(IUserService userService, UserManager<IdentityUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [BindProperty]
    public string NewUsername { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = _userManager.GetUserId(User);

        var result = await _userService.ChangeUsernameAsync(userId, NewUsername);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return Page();
        }

        return RedirectToPage("/Account/Index");
    }
}
