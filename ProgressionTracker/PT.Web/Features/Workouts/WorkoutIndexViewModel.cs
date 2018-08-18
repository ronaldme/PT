using X.PagedList;

namespace PT.Web.Features.Workouts
{
    public class WorkoutIndexViewModel
    {
        public IPagedList<WorkoutViewModel> PagedList { get; set; }
    }
}