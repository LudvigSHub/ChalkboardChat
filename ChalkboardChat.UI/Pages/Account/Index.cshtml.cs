using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AccountModel : PageModel
{
    private readonly IUserService _userService;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountModel(IUserService userService, UserManager<IdentityUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var userId = _userManager.GetUserId(User);

        var result = await _userService.DeleteAccountAsync(userId);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return Page();
        }

        return RedirectToPage("/Index");
    }
}