using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WheelOfFortune.Shared.Model;

namespace WheelOfFortune.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;
        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Landlord")]
        public async Task<IActionResult> Post([FromForm] IFormFile Image)
        {
            if (Image.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\images\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var extension = Path.GetExtension(Image.FileName);
                if (extension == ".jpg" | extension == ".jpeg" | extension == ".png")
                {
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var imagePath = Path.Combine(path, fileName);

                    using (var fs = new FileStream(imagePath, FileMode.Create))
                    {
                        await Image.OpenReadStream().CopyToAsync(fs);
                        return Ok("/images/" + fileName);
                    }
                }
                else
                {
                    return BadRequest("Not valid extension");
                }
            }
            return BadRequest();
        }
    }
}
