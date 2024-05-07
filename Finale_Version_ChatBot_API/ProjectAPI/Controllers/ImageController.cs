using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Helpers;
using QuestionsAndAnswers.DataLayer.Models;
using QuestionsAndAnswers.DataLayer.Repository;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ChatBotContext _context;
        public ImageController(ChatBotContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<ImageModel> GetAll()
        {
            return _context.ImageModel;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file/*, string fileName*/)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] imageData = memoryStream.ToArray();

                    var imageModal = new ImageModel 
                    {
                        Filename = file.FileName,
                        Data = imageData
                    };
                    _context.ImageModel.Add(imageModal);
                    await _context.SaveChangesAsync();
                    return Ok(imageModal.Id);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to upload image:{ex.Message}");
            }


            /*_context.Image.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImage), new { id = model.Id }, model);*/

        }
        [HttpGet("{id}")]
        public IActionResult GetImage(long id)
        {
            var image = _context.ImageModel.FirstOrDefault(i=>i.Id==id);

            if (image == null)
            {
                return NotFound();
            }
            return File(image.Data,"image/jpeg",image.Filename);
        }
        [HttpDelete("{id}")]
        public void DeleteAppeal(long id)
        {
            ImageModel appeal = _context.ImageModel.Find(id);
            if (appeal != null)
            {
                _context.ImageModel.Remove(appeal);
                _context.SaveChanges();
            }
        }
    }
}
