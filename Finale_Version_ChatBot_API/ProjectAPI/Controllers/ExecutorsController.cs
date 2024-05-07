using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Helpers;
using QuestionsAndAnswers.DataLayer.Models;
using QuestionsAndAnswers.DataLayer.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutorsController : ControllerBase
    {
        private readonly ChatBotContext db;
        public ExecutorsController(ChatBotContext _db)
        {
            db = _db;
        }
        // GET: api/<RolesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Executor> GetAll()
        {
            return db.Executor;
        }

        // GET api/<RolesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public Executor GetRole(long id)
        {
            Executor SN = db.Executor.Find(id);
            return SN;
        }

        // POST api/<RolesController>
        [Authorize]
        [HttpPost]
        public void CreateRole([FromBody] Executor SN)
        {
            db.Executor.Add(SN);
            db.SaveChanges();
        }

        // PUT api/<RolesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void EditRole(long id, [FromBody] Executor SN)
        {
            if (id == SN.Id)
            {
                db.Entry(SN).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/<RolesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteRole(long id)
        {
            Executor SN = db.Executor.Find(id);
            if (SN != null)
            {
                db.Executor.Remove(SN);
                db.SaveChanges();
            }
        }
    }
}
