using System;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class WeightRegistration : Entity
    {
        public float Weight { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
    }
}