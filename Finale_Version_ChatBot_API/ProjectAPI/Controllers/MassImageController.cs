using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Helpers;
using QuestionsAndAnswers.DataLayer.Models;
using QuestionsAndAnswers.DataLayer.Repository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassImageController : ControllerBase
    {

        //ChatBotContext db = new ChatBotContext();
        private readonly ChatBotContext _context;
        public MassImageController(ChatBotContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<MassImages> GetAll()
        {
            return _context.MassImages;
        }
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<MassImages> GetMassImage(long id)
        {
            var massImage = _context.MassImages.Find(id);
            if (massImage == null)
            {
                return NotFound();
            }
            return massImage;
        }
        [Authorize]
        [HttpPost]
        public ActionResult<MassImages> PostMassImage(MassImages massImages) 
        {
            _context.MassImages.Add(massImages);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMassImage),new {id = massImages.Id },massImages);

        }
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteAppeal(long id)
        {
            MassImages appeal = _context.MassImages.Find(id);
            if (appeal != null)
            {
                _context.MassImages.Remove(appeal);
                _context.SaveChanges();
            }
        }
    }
}
