using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PT.DAL.Entities;
using PT.Web.Features.User;

namespace PT.Web.Areas.Identity.Pages.Account.Manage
{
    public class UserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public int Id { get; set; }
        public string Name { get; set; }
        public string AspNetUsersId { get; set; }
        public long? TelegramChatId { get; set; }

        public UserModel(UserManager<IdentityUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            AspNetUsersId = user.Id;

            var res = await _mediator.Send(new UserQuery {AspNetUsersId = user.Id});           
            if (res == null) return;
            Id = res.Id;
            Name = res.Name;
            TelegramChatId = res.TelegramChatId;
        }

        public async Task<IActionResult> OnPostCreate(CreateUserCommand command)
        {
            await _mediator.Send(command);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdate(UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return RedirectToPage();
        }
    }
}