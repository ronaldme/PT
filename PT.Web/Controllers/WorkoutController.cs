using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;
using X.PagedList;

namespace PT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly PtDbContext _db;

        public WorkoutController(PtDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("/workout/overview")]
        public async Task<IPagedList<Workout>> Overview(int pageNumber, int pageSize)
        {
            return await _db.Workouts
                .OrderBy(wi => wi.Name)
                .ToPagedListAsync(pageNumber, pageSize);
        }

        [HttpGet]
        [Route("/workout/list")]
        public async Task<List<WorkoutItem>> List()
        {
            return await _db.Workouts
                .OrderBy(wi => wi.Name)
                .Select(wi => new WorkoutItem
                {
                    Id = wi.Id,
                    Name = wi.Name
                })
                .ToListAsync();
        }

        [HttpPost]
        [Route("/workout/add")]
        public async Task Add(AddWorkout item)
        {
            _db.Workouts.Add(new Workout
            {
                Name = item.Name,
            });

            await _db.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/workout/edit")]
        public async Task Edit(EditWorkout item)
        {
            var workout = await _db.Workouts.SingleAsync(w => w.Id == item.Id);
            workout.Name = item.Name;

            await _db.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/workout/delete")]
        public async Task Delete(DeleteWorkoutModel model)
        {
            if (_db.WorkoutCalenderItem.Any(wci => wci.WorkoutId == model.Id))
                throw new Exception($"Cannot delete {nameof(Workout)} with Id: {model.Id} because it is used in a {nameof(WorkoutCalenderItem)}");

            var workout = await _db.Workouts.SingleAsync(w => w.Id == model.Id);
            _db.Workouts.Remove(workout);

            await _db.SaveChangesAsync();
        }

        public class AddWorkout
        {
            public string Name { get; set; }
        }

        public class EditWorkout
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class DeleteWorkoutModel
        {
            public int Id { get; set; }
        }

        public class WorkoutItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}