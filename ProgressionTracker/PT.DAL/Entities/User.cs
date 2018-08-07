using System.Collections.Generic;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class User : Entity
    {
        public virtual string Name { get; set; }
        public virtual ICollection<TrainingWeek> Trainings { get; set; }
    }
}