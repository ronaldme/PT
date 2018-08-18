using System.Collections.Generic;
using MediatR;

namespace PT.Web.Features.Workouts
{
    public class WorkoutsQuery : IRequest<List<WorkoutViewModel>>
    {

    }
}