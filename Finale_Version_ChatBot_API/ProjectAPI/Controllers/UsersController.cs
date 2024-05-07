using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Helpers;
using QuestionsAndAnswers.DataLayer.Models;
using QuestionsAndAnswers.DataLayer.Repository;
namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //ChatBotContext db = new ChatBotContext();
        private readonly ChatBotContext db;
        public UsersController(ChatBotContext _db)
        {
            db = _db;
        }

        // GET: api/<AppealsController>
        [Authorize]
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        // GET api/<AppealsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public User GetAppeal(long id)
        {
            User appeal = db.Users.Find(id);
            return appeal;
        }

        // POST api/<AppealsController>
        [Authorize]
        [HttpPost]
        public void CreateAppeal([FromBody] User appeal)
        {
            db.Users.Add(appeal);
            db.SaveChanges();
        }

        // PUT api/<AppealsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void EditAppeal(long id, [FromBody] User appeal)
        {
            if (id == appeal.Id)
            {
                db.Entry(appeal).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/<AppealsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteAppeal(long id)
        {
            User appeal = db.Users.Find(id);
            if (appeal != null)
            {
                db.Users.Remove(appeal);
                db.SaveChanges();
            }
        }
    }
}
