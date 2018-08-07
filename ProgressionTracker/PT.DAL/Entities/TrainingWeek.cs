using System.Collections.Generic;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class TrainingWeek : Entity
    {
        public virtual int Year { get; set; }
        public virtual int WeekNumber { get; set; }
        public virtual ICollection<TrainingDay> TrainingsDays { get; set; }
    }
}