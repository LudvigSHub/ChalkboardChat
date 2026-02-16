using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ChangePasswordModel : PageModel
{
    private readonly IUserService _userService;
    private readonly UserManager<IdentityUser> _userManager;

    public ChangePasswordModel(IUserService userService, UserManager<IdentityUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [BindProperty]
    public string OldPassword { get; set; }

    [BindProperty]
    public string NewPassword { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = _userManager.GetUserId(User);

        var result = await _userService.ChangePasswordAsync(userId, OldPassword, NewPassword);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return Page();
        }

        return RedirectToPage("/Account/Index");
    }
}
