using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PT.Web.Features.Workouts
{
    public class UpsertWorkoutCommand : IRequest
    {
        public int? Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public int SelectedWorkoutType { get; set; }
    }
}