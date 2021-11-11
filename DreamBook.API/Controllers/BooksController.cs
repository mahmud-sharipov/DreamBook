using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Books;
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
    public class BooksController : ControllerBase
    {
        public IBookService Service { get; }

        public BooksController(IBookService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<BookResponseModel>>> GetPagedList([FromQuery] PagedListRequestModel<BookTranslation> requestModel)
        {
            return Ok(await Service.GetPagedList(requestModel));
        }

        [RequireAdmin]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BookResponseModel>>> GetAll()
        {
            return Ok(await Service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseModel>> GetById([FromRoute] Guid id)
        {
            return Ok(await Service.GetById(id));
        }

        [HttpGet("with-all-translations/{id}")]
        public async Task<ActionResult<BookWithTranslationsResponseModel>> GetByIdWithAllTranslations([FromRoute] Guid id)
        {
            return Ok(await Service.GetByIdWithTranslations(id));
        }

        [HttpPost]
        [RequireModerator]
        public async Task<ActionResult<BookResponseModel>> Create([FromBody] CreateBookRequestModel model)
        {
            var responce = await Service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
        }

        [HttpPut]
        [RequireModerator]
        public async Task<IActionResult> Update([FromBody] UpdateBookRequestModel model)
        {
            await Service.Update(model);
            return NoContent();
        }

        [RequireModerator]
        [HttpDelete("{id}")]
        public async virtual Task<IActionResult> Delete(Guid id)
        {
            await Service.Delete(id);
            return NoContent();
        }
    }
}