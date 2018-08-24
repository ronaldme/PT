using MediatR;
using X.PagedList;

namespace PT.Web.Features.Exercises
{
    public class ExerciseIndexQuery : IRequest<IPagedList<ExerciseViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}