using Microsoft.AspNetCore.Cors;
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
    public class SoftwareNamesController : ControllerBase
    {
        private readonly ChatBotContext db;
        public SoftwareNamesController(ChatBotContext _db)
        {
            db = _db;
        }
        // GET: api/<SoftwareNamesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<SoftwareName> GetAll()
        {
            return db.softwareNames;
        }

        // GET api/<SoftwareNamesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public SoftwareName GetSoftwareName(long id)
        {
            SoftwareName SN = db.softwareNames.Find(id);
            return SN;
        }

        // POST api/<SoftwareNamesController>
        [Authorize]
        [HttpPost]
        public void CreateSoftwareName([FromBody] SoftwareName SN)
        {
            db.softwareNames.Add(SN);
            db.SaveChanges();
        }

        // PUT api/<SoftwareNamesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void EditSoftwareName(long id, [FromBody] SoftwareName SN)
        {
            if (id == SN.Id)
            {
                db.Entry(SN).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/<SoftwareNamesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteSoftwareName(long id)
        {
            SoftwareName SN = db.softwareNames.Find(id);
            if (SN != null)
            {
                db.softwareNames.Remove(SN);
                db.SaveChanges();
            }
        }
    }
}
