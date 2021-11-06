﻿using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Interpretations;
using DreamBook.Application.Words;
using DreamBook.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DreamBook.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly IInterpretationService _interpretationService;
        public IWordService Service { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WordsController(IWordService service, IInterpretationService interpretationService, IWebHostEnvironment webHostEnvironment)
        {
            Service = service;
            _interpretationService = interpretationService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<WordResponseModel>>> GetPagedList([FromQuery] PagedListRequestModel<WordTranslation> requestModel)
        {
            return Ok(await Service.GetPagedList(requestModel));
        }

        [HttpGet("zip")]
        public async Task<ActionResult> GetZip([FromQuery] Guid laguage)
        {
            var result = await Service.GetAllByLanguageInJson(laguage);
            var fileName = $"words_{laguage}_{DateTime.Now.ToFileTimeUtc()}.json";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Files");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePaht = Path.Combine(path, fileName);
            System.IO.File.WriteAllText(filePaht, result);
            byte[] resultByte = System.IO.File.ReadAllBytes(filePaht);
            System.IO.File.Delete(filePaht);
            return File(resultByte, "application/json", $"Words_{laguage}.json");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordResponseModel>> GetById([FromRoute] Guid id)
        {
            return Ok(await Service.GetById(id));
        }

        [HttpGet("{id}/interpretations")]
        public async Task<ActionResult<WordResponseModel>> GetInterpretationsById([FromRoute] Guid id)
        {
            return Ok(await _interpretationService.GetByWordId(id));
        }

        [HttpGet("with-all-translations/{id}")]
        public async Task<ActionResult<WordWithTranslationsResponseModel>> GetByIdWithAllTranslations([FromRoute] Guid id)
        {
            return Ok(await Service.GetByIdWithTranslations(id));
        }

        [HttpPost]
        public async Task<ActionResult<WordResponseModel>> Create([FromBody] CreateWordRequestModel model)
        {
            var responce = await Service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWordRequestModel model)
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