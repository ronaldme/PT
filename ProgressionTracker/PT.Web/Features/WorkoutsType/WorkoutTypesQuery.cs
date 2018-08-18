using System.Collections.Generic;
using MediatR;
using PT.Web.Features.Workouts;

namespace PT.Web.Features.WorkoutsType
{
    public class WorkoutTypesQuery : IRequest<List<WorkoutTypeViewModel>>
    {

    }
}