using MediatR;

namespace PT.Web.Features.Workouts
{
    public class SetRemarkCommand : IRequest
    {
        public int Id { get; set; }
        public string Remark { get; set; }
    }
}