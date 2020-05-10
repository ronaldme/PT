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
    public class WorkoutController : ControllerBase
    {
        private readonly PtDbContext _db;

        public WorkoutController(PtDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Workout> List() => _db.Workouts.ToList();

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
            var workout = await _db.Workouts.SingleOrDefaultAsync(w => w.Id == item.Id);
            workout.Name = item.Name;

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
    }
}