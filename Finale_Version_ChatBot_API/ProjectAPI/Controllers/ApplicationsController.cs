using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionsAndAnswers.DataLayer.Repository;
using QuestionsAndAnswers.DataLayer.Models;
using ProjectAPI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ChatBotContext db;
        public ApplicationsController(ChatBotContext _db)
        {
            db = _db;
        }
        // GET: api/<ApplicationsController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Application> GetAll()
        {
            return db.Application;
        }

        // GET api/<ApplicationsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public Application GetApplication(long id)
        {
            Application SN = db.Application.Find(id);
            return SN;
        }

        // POST api/<ApplicationsController>
        [Authorize]
        [HttpPost]
        public void CreateApplication([FromBody] Application SN)
        {
            db.Application.Add(SN);
            db.SaveChanges();
        }

        // PUT api/<ApplicationsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void EditApplication(long id, [FromBody] Application SN)
        {
            if (id == SN.Id)
            {
                db.Entry(SN).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/<ApplicationsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteApplication(long id)
        {
            Application SN = db.Application.Find(id);
            if (SN != null)
            {
                db.Application.Remove(SN);
                db.SaveChanges();
            }
        }
    }
}
