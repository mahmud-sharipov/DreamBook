using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace DreamBook.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName ?? "");
            if (System.IO.File.Exists(path))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", fileName);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult GetPagedList(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "." + file.FileName.Split(".").Last();
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
                file.CopyTo(fileStream);

            return Ok(uniqueFileName);
        }
    }
}