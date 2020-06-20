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
    public class WeightRegistrationController : ControllerBase
    {
        private readonly PtDbContext _db;

        public WeightRegistrationController(PtDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("/weightRegistration/list")]
        public List<WeightRegistration> List() => _db.WeightRegistrations.ToList();

        [HttpPost]
        [Route("/weightRegistration/add")]
        public async Task Add(AddWeightRegistration item)
        {
            _db.WeightRegistrations.Add(new WeightRegistration
            {
                Weight = item.Weight,
                RegistrationDate = item.RegistrationDate,
            });

            await _db.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/weightRegistration/delete")]
        public async Task Delete(DeleteWeightRegistration data)
        {
            var registration = await _db.WeightRegistrations.SingleAsync(t => t.Id == data.Id);
            _db.WeightRegistrations.Remove(registration);
            await _db.SaveChangesAsync();
        }

        public class AddWeightRegistration
        {
            public DateTimeOffset RegistrationDate { get; set; }
            public float Weight { get; set; }
        }

        public class DeleteWeightRegistration
        {
            public int Id { get; set; }
        }
    }
}