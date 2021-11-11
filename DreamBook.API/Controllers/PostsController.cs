using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.App.Posts.RequestModels;
using DreamBook.Application.Posts;
using DreamBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamBook.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        public IPostService Service { get; }

        public PostsController(IPostService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<PostResponseModel>>> GetPagedList([FromQuery] PostPagedListRequestModel requestModel)
        {
            return Ok(await Service.GetPagedList(requestModel));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostResponseModel>>> GetAll()
        {
            return Ok(await Service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseModel>> GetById([FromRoute] Guid id)
        {
            return Ok(await Service.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<PostResponseModel>> Create([FromBody] CreatePostRequestModel model)
        {
            var responce = await Service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePostRequestModel model)
        {
            await Service.Update(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async virtual Task<IActionResult> Delete(Guid id)
        {
            await Service.Delete(id);
            return NoContent();
        }
    }
}