using AutoMapper;
using DreamBook.Application.PostCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DreamBook.WebApplication.Controllers
{
    [AllowAnonymous]
    public class PostCategoriesController : Controller
    {
        public IPostCategoryService Service { get; }
        public IMapper Mapper { get; }

        public PostCategoriesController(IPostCategoryService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
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
        public async Task<ActionResult> Create(CreatePostCategoryRequestModel queryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
            var queryModel = Mapper.Map<UpdatePostCategoryRequestModel>(viewModel);
            return View(queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] Guid id, UpdatePostCategoryRequestModel queryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
