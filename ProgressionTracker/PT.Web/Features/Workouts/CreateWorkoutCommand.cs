using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PT.Web.Features.Workouts
{
    public class CreateWorkoutCommand : IRequest
    {
        public string UserId { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public int SelectedWorkoutType { get; set; }
    }
}