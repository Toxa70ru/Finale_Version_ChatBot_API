
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
    public class QuestuionAnswersController : ControllerBase
    {
        private readonly ChatBotContext db;
        public QuestuionAnswersController(ChatBotContext _db)
        {
            db = _db;
        }
        // GET: api/<QuestuionAnswersController>
        [Authorize]
        [HttpGet]
        public IEnumerable<QuestionAnswer> GetAll()
        {
            return db.QuestionAnswer;
        }
        [Authorize]
        [HttpGet("search")]
        public IEnumerable<QuestionAnswer> Search(string searchString)
        {
            //TODO: Сделать возврат строки в верхнем регистре
            return db.QuestionAnswer.Where(x => x.Question.Contains(searchString));
        }

        // GET api/<QuestuionAnswersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public QuestionAnswer GetQuestionAnswer(long id)
        {
            QuestionAnswer SN = db.QuestionAnswer.Find(id);
            return SN;
        }

        // POST api/<QuestuionAnswersController>
        [Authorize]
        [HttpPost]
        public void CreateQuestionAnswer([FromBody] QuestionAnswer SN)
        {
            db.QuestionAnswer.Add(SN);
            db.SaveChanges();
        }

        // PUT api/<QuestuionAnswersController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void EditQuestionAnswer(long id, [FromBody] QuestionAnswer SN)
        {
            if (id == SN.Id)
            {
                db.Entry(SN).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/<QuestuionAnswersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteQuestionAnswer(long id)
        {
            QuestionAnswer SN = db.QuestionAnswer.Find(id);
            if (SN != null)
            {
                db.QuestionAnswer.Remove(SN);
                db.SaveChanges();
            }
        }
    }
}
