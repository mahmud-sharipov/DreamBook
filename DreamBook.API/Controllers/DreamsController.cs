using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Dreams;
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
    public class DreamsController : ControllerBase
    {
        public IDreamService Service { get; }

        public DreamsController(IDreamService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<DreamResponseModel>>> GetPagedList([FromQuery] PagedListRequestModel<Dream> requestModel)
        {
            return Ok(await Service.GetPagedList(requestModel));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DreamResponseModel>>> GetAll()
        {
            return Ok(await Service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DreamResponseModel>> GetById([FromRoute] Guid id)
        {
            return Ok(await Service.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<DreamResponseModel>> Create([FromBody] CreateDreamRequestModel model)
        {
            var responce = await Service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDreamRequestModel model)
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