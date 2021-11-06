using AutoMapper;
using DreamBook.Application.Ads;
using DreamBook.WebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DreamBook.WebApplication.Controllers
{
    [AllowAnonymous]
    public class AdsController : Controller
    {
        public IAdService Service { get; }
        public IMapper Mapper { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        public AdsController(IAdService service, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            Service = service;
            Mapper = mapper;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var allTypes = await Service.GetAll();
            return View(allTypes);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var viewModel = await Service.GetByIdWithTranslations(id);
            return View(viewModel);
        }

        public async Task<ActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAdWithImageRequestModel queryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    queryModel.Image = UploadedFile(queryModel.ImageFile);
                    var viewModel = await Service.Create(queryModel);
                    return RedirectToAction(nameof(Details), new { Id = viewModel.Guid });
                }
                else
                {
                    return View(queryModel);
                }
            }
            catch
            {
                return View(queryModel);
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await Service.GetByIdWithTranslations(id);
            var queryModel = Mapper.Map<UpdateAdWithImageRequestModel>(viewModel);
            return View(queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] Guid id, UpdateAdWithImageRequestModel queryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    queryModel.Image = UploadedFile(queryModel.ImageFile);
                    queryModel.Guid = id;
                    await Service.Update(queryModel);
                    return RedirectToAction(nameof(Details), new { Id = id });
                }
                else
                {
                    return View(queryModel);
                }
            }
            catch
            {
                return View(queryModel);
            }
        }

        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images\\ads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public ActionResult GetAdImage([FromQuery] string fileName)
        {
            string path = Path.Combine(WebHostEnvironment.WebRootPath, "images\\ads\\") + fileName;
            if (System.IO.File.Exists(path))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", fileName);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await Service.Delete(id);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
