using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutCalenderController : ControllerBase
    {
        private readonly PtDbContext _db;

        public WorkoutCalenderController(PtDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<WorkoutCalenderItem> List()
        {
            var startDate = DateTime.Now.AddDays(-3);
            return _db.WorkoutCalenderItem
                .Include(wci => wci.Workout)
                .OrderBy(wci => wci.Date)
                .Where(wci => wci.Date > startDate)
                .ToList();
        }

        [HttpGet]
        [Route("/workoutCalender/history")]
        public List<WorkoutCalenderItem> History() // TODO: Add pagination
        {
            return _db.WorkoutCalenderItem
                .Include(wci => wci.Workout)
                .OrderBy(wci => wci.Date)
                .ToList();
        }

        [HttpPost]
        [Route("/workoutCalender/add")]
        public async Task Add(AddWorkoutCalenderItem item)
        {
            _db.WorkoutCalenderItem.Add(new WorkoutCalenderItem
            {
                WorkoutId = item.WorkoutId,
                Date = item.DateTime.Date,
            });

            await _db.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/workoutCalender/toggleIsCompleted")]
        public async Task ToggleIsCompleted(ToggleData data)
        {
            var workoutCalenderItem =
                await _db.WorkoutCalenderItem.SingleOrDefaultAsync(t => t.Id == data.WorkoutCalenderItemId) ??
                throw new Exception($"Couldn't find {nameof(WorkoutCalenderItem)} with id: {data.WorkoutCalenderItemId}");

            workoutCalenderItem.IsCompleted = data.IsCompleted;

            await _db.SaveChangesAsync();
        }
    }

    public class AddWorkoutCalenderItem
    {
        public DateTimeOffset DateTime { get; set; }
        public int WorkoutId { get; set; }
    }

    public class ToggleData
    {
        public int WorkoutCalenderItemId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
