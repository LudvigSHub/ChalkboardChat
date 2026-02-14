using ChalkboardChat.DAL.Data;
using ChalkboardChat.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MessagesModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public MessagesModel(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<MessageModel> Messages { get; set; }
    [BindProperty]
    public string NewMessage { get; set; }

    public void OnGet()
    {
        Messages = _context.Messages
    .OrderBy(m => m.Date)
    .ToList();

    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(NewMessage))
            return RedirectToPage();

        var user = _userManager.GetUserAsync(User).Result;

        var msg = new MessageModel
        {
            Message = NewMessage,
            Username = user.UserName,
            Date = DateTime.Now
        };

        _context.Messages.Add(msg);
        _context.SaveChanges();

        return RedirectToPage();
    }
}