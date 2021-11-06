using AutoMapper;
using DreamBook.Application.PostCategories;
using DreamBook.Application.Posts;
using DreamBook.WebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.WebApplication.Controllers
{
    [AllowAnonymous]
    public class PostsController : Controller
    {
        private readonly IPostService Service;
        private readonly IPostCategoryService CategoryService;
        private readonly IMapper Mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostsController(IPostService service, IMapper mapper, IPostCategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            Service = service;
            Mapper = mapper;
            CategoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var allTypes = await Service.GetAll();
            return View(allTypes.OfType<PostResponseModel>());
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var viewModel = await Service.GetById(id);
            return View(viewModel);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = (await CategoryService.GetAll()).OfType<PostCategoryTranslationResponseModel>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePostWithImageRequestModel queryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UploadedFile(queryModel.ImageFile);
                    var viewModel = await Service.Create(queryModel);
                    return RedirectToAction(nameof(Details), new { Id = viewModel.Guid });
                }
                else
                {
                    ViewBag.Categories = (await CategoryService.GetAll()).OfType<PostCategoryTranslationResponseModel>();
                    return View(queryModel);
                }
            }
            catch
            {
                ViewBag.Categories = (await CategoryService.GetAll()).OfType<PostCategoryTranslationResponseModel>();
                return View(queryModel);
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await Service.GetById(id);
            var queryModel = Mapper.Map<UpdatePostWithImageRequestModel>(viewModel);
            ViewBag.Categories = (await CategoryService.GetAll()).OfType<PostCategoryTranslationResponseModel>();
            return View(queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] Guid id, UpdatePostWithImageRequestModel queryModel)
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
                    ViewBag.Categories = (await CategoryService.GetAll()).OfType<PostCategoryTranslationResponseModel>();
                    return View(queryModel);
                }
            }
            catch
            {
                ViewBag.Categories = (await CategoryService.GetAll()).OfType<PostCategoryTranslationResponseModel>();
                return View(queryModel);
            }
        }

        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\posts");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public ActionResult GetPostImage([FromQuery] string fileName)
        {
            string path = Path.Combine(webHostEnvironment.WebRootPath, "images\\posts\\") + fileName;
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
