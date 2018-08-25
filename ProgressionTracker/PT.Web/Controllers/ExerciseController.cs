using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PT.Web.Features.Exercises;

namespace PT.Web.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(ExerciseIndexQuery query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(AddEditExerciseQuery query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(UpsertExerciseCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(DeleteExerciseCommand command)
        {
            _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}