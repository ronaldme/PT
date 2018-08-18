using System.Collections.Generic;
using PT.Web.Features.Exercises;

namespace PT.Web.Features.WorkoutsType
{
    public class WorkoutTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ExerciseViewModel> Exercises { get; set; }
    }
}