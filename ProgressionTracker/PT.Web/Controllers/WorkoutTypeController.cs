using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT.Web.Features.WorkoutsType;

namespace PT.Web.Controllers
{
    [Authorize]
    public class WorkoutTypeController : Controller
    {
        private readonly IMediator _mediator;

        public WorkoutTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(WorkoutTypesQuery query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkoutTypeCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}