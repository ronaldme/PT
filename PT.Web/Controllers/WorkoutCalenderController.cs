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
        public async Task<IPagedList<WorkoutCalenderItem>> History(PagedListModel model) // TODO: Add pagination
        {
            return await _db.WorkoutCalenderItem
                .Include(wci => wci.Workout)
                .OrderBy(wci => wci.Date)
                .ToPagedListAsync(model.PageNumber, model.PageSize);
        }

        [HttpGet]
        [Route("/workoutCalender/yearlyHistory/{year}")]
        public async Task<List<WorkoutCalenderItem>> YearlyHistory(int year)
        {
            return await _db.WorkoutCalenderItem
                .Include(wci => wci.Workout)
                .Where(wci => wci.Date.Year == year)
                .OrderBy(wci => wci.Date)
                .ToListAsync();
        }

        [HttpPost]
        [Route("/workoutCalender/add")]
        public async Task Add(AddWorkoutCalenderItem item)
        {
            _db.WorkoutCalenderItem.Add(new WorkoutCalenderItem
            {
                WorkoutId = item.WorkoutId,
                Date = item.Date.ToLocalTime().Date,
                Remark = item.Remark,
                Distance = item.Distance,
            });

            await _db.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/workoutCalender/addRemark")]
        public async Task Add(AddRemark item)
        {
            var workoutCalenderItem = await _db.WorkoutCalenderItem.SingleOrDefaultAsync(t => t.Id == item.Id) ??
                throw new Exception($"Couldn't find {nameof(WorkoutCalenderItem)} with id: {item.Id}");

            workoutCalenderItem.Remark = item.Remark;
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

        [HttpPost]
        [Route("/workoutCalender/delete")]
        public async Task Delete(DeleteWorkoutCalenderItemModel data)
        {
            var item = await _db.WorkoutCalenderItem.SingleAsync(t => t.Id == data.Id);
            if (IsValidForDeletion()) 
                throw new Exception($"Cannot remove {nameof(WorkoutCalenderItem)} when the workout is completed and {data.ForceDelete} is false");

            _db.WorkoutCalenderItem.Remove(item);
            await _db.SaveChangesAsync();

            bool IsValidForDeletion() => item.IsCompleted && !data.ForceDelete;
        }
    }

    public class AddWorkoutCalenderItem
    {
        public DateTimeOffset Date { get; set; }
        public int WorkoutId { get; set; }
        public string Remark { get; set; }
        public float? Distance { get; set; }
    }

    public class ToggleData
    {
        public int WorkoutCalenderItemId { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class PagedListModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class DeleteWorkoutCalenderItemModel
    {
        public int Id { get; set; }
        public bool ForceDelete { get; set; }
    }

    public class AddRemark
    {
        public int Id { get; set; }
        public string Remark { get; set; }
    }
}
