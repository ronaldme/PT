using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PT.Web.Features.Workouts;

namespace PT.Web.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public WorkoutController(UserManager<IdentityUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await _mediator.Send(new WorkoutIndexQuery
            {
                UserId = user.Id,
                PageSize = 10,
                PageNumber = 1
            });
            
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CreateWorkoutQuery query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkoutCommand command)
        {
            command.UserId = _userManager.GetUserId(User);
            await _mediator.Send(command);
            return RedirectToAction(nameof(Create));
        }
    }
}