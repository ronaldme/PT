using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PT.Web.Features.PlannedWorkouts;
using PT.Web.Features.Workouts;

namespace PT.Web.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IMediator _mediator;

        public WorkoutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkoutCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PlannedWorkouts(PlannedWorkoutsQuery query)
        {
            return View(await _mediator.Send(query));
        }
    }
}