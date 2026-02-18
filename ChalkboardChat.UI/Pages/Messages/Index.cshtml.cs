using ChalkboardChat.BLL.Interfaces;
using ChalkboardChat.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public IndexModel(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public List<MessageModel> Messages { get; set; }

        [BindProperty]
        public string NewMessage { get; set; }

        public async Task OnGet()
        {
            Messages = await _messageService.GetMessagesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userService.GetCurrentUserId(); // <-- Angelos metod

            var result = await _messageService.SendMessageAsync(userId, NewMessage);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                Messages = await _messageService.GetMessagesAsync();
                return Page();
            }

            return RedirectToPage();
        }
    }
}