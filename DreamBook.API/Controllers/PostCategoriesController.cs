using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.PostCategories;
using DreamBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamBook.API.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class PostCategoriesController : ControllerBase
    {
        public IPostCategoryService Service { get; }

        public PostCategoriesController(IPostCategoryService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<PostCategoryResponseModel>>> GetPagedList([FromQuery] PagedListRequestModel<PostCategoryTranslation> requestModel)
        {
            return Ok(await Service.GetPagedList(requestModel));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostCategoryResponseModel>>> GetAll()
        {
            return Ok(await Service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostCategoryResponseModel>> GetById([FromRoute] Guid id)
        {
            return Ok(await Service.GetById(id));
        }

        [HttpGet("with-all-translations/{id}")]
        public async Task<ActionResult<PostCategoryWithTranslationsResponseModel>> GetByIdWithAllTranslations([FromRoute] Guid id)
        {
            return Ok(await Service.GetByIdWithTranslations(id));
        }

        [HttpPost]
        [RequireModerator]
        public async Task<ActionResult<PostCategoryResponseModel>> Create([FromBody] CreatePostCategoryRequestModel model)
        {
            var responce = await Service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
        }

        [HttpPut]
        [RequireModerator]
        public async Task<IActionResult> Update([FromBody] UpdatePostCategoryRequestModel model)
        {
            await Service.Update(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [RequireModerator]
        public async virtual Task<IActionResult> Delete(Guid id)
        {
            await Service.Delete(id);
            return NoContent();
        }
    }
}