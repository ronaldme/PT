using MediatR;

namespace PT.Web.Features.WorkoutsType
{
    public class DeleteWorkoutTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}