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
            var res = await _mediator.Send(new WorkoutsQuery{ UserId = user.Id, UpcomingWorkouts = true});
            return View(res);
        }

        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await _mediator.Send(new WorkoutsQuery { UserId = user.Id });
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(AddEditWorkoutQuery query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(UpsertWorkoutCommand command)
        {
            command.UserId = _userManager.GetUserId(User);
            await _mediator.Send(command);
            return RedirectToAction(nameof(AddEdit));
        }

        public async Task<IActionResult> Delete(DeleteWorkoutCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetFinishedIndex(SetFinishedCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetFinished(SetFinishedCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(History));
        }

        public async Task<IActionResult> SetRemark(SetRemarkCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(History));
        }
    }
}