using System.Collections;
using System.Collections.Generic;
using MediatR;

namespace PT.Web.Features.PlannedWorkouts
{
    public class PlannedWorkoutsQuery : IRequest<List<PlannedWorkoutViewModel>>
    {

    }
}