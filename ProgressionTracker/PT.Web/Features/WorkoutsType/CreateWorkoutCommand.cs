using MediatR;

namespace PT.Web.Features.WorkoutsType
{
    public class CreateWorkoutTypeCommand : IRequest
    {
        public string Name { get; set; }
    }
}