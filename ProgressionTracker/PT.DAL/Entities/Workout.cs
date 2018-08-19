using System;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class Workout : Entity
    {
        public virtual User User { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual WorkoutType WorkoutType { get; set; }
        public virtual bool Finished { get; set; }
        public virtual string Remark { get; set; }
        public virtual int WorkoutTypeId { get; set; }
    }
}