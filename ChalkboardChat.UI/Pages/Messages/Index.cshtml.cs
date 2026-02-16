using ChalkboardChat.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MessagesModel : PageModel
{
    private readonly IMessageService _messageService;
    private readonly UserManager<IdentityUser> _userManager;

    public MessagesModel(IMessageService messageService, UserManager<IdentityUser> userManager)
    {
        _messageService = messageService;
        _userManager = userManager;
    }

    public List<MessageModel> Messages { get; set; }

    [BindProperty]
    public string NewMessage { get; set; }

    public async Task OnGet()
    {
        Messages = await _messageService.GetMessagesAsync();
    }

    public async Task<IActionResult> OnPost()
    {
        var userId = _userManager.GetUserId(User);
        await _messageService.SendMessageAsync(userId, NewMessage);
        return RedirectToPage();
    }
}
