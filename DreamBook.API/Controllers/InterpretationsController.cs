using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Interpretations;
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
    public class InterpretationsController : ControllerBase
    {
        public IInterpretationService Service { get; }

        public InterpretationsController(IInterpretationService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<InterpretationResponseModel>>> GetPagedList([FromQuery] InterpretationPagedListRequestModel requestModel)
        {
            return Ok(await Service.GetPagedList(requestModel));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<InterpretationResponseModel>>> GetAll()
        {
            return Ok(await Service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterpretationResponseModel>> GetById([FromRoute] Guid id)
        {
            return Ok(await Service.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<InterpretationResponseModel>> Create([FromBody] CreateInterpretationRequestModel model)
        {
            var responce = await Service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateInterpretationRequestModel model)
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