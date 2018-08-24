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

        public async Task<IActionResult> AddEdit(AddEditExerciseQuery query)
        {
            return View(await _mediator.Send(query));
        }
    }
}