﻿using Microsoft.AspNetCore.Mvc;
using QuestionsAndAnswers.DataLayer.Repository;
using QuestionsAndAnswers.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly ChatBotContext db;
        public StatusesController(ChatBotContext _db)
        {
            db = _db;
        }
        // GET: api/<StatusesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Status> GetAll()
        {
            return db.Status;
        }

        // GET api/<StatusesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public Status GetStatus(long id)
        {
            Status SN = db.Status.Find(id);
            return SN;
        }

        // POST api/<StatusesController>
        [Authorize]
        [HttpPost]
        public void CreateStatus([FromBody] Status SN)
        {
            db.Status.Add(SN);
            db.SaveChanges();
        }

        // PUT api/<StatusesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void EditStatus(long id, [FromBody] Status SN)
        {
            if (id == SN.Id)
            {
                db.Entry(SN).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/<StatusesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteStatus(long id)
        {
            Status SN = db.Status.Find(id);
            if (SN != null)
            {
                db.Status.Remove(SN);
                db.SaveChanges();
            }
        }
    }
}
