using System.Collections.Generic;
using System.Linq;
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
            return _db.WorkoutCalenderItem
                .Include(wci => wci.Workout)
                .OrderBy(wci => wci.Date)
                .ToList();
        }
    }
}
