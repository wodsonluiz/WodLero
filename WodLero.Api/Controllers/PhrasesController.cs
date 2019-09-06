using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WodLero.Domain.Entities;
using WodLero.Domain.Interface;

namespace WodLero.Api.Controllers
{
    [Route("api/Phrases")]
    [ApiController]
    public class PhrasesController : Controller
    {
        protected readonly IPhrasesRepository phrasesRepository;
        private IConfiguration _config;

        public PhrasesController(IConfiguration _config, IPhrasesRepository phrasesRepository)
        {
            this._config = _config;
            this.phrasesRepository = phrasesRepository;
        }


        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Phrases> GetAll()
        {
            return phrasesRepository.GetAll(_config.GetConnectionString("ExemploDapperSqlServer"));
        }

        [HttpGet]
        [Route("GetById")]
        public Phrases GetById(int Id)
        {
            return phrasesRepository.GetById(Id, _config.GetConnectionString("ExemploDapperSqlServer"));
        }

        [HttpPost]
        [Route("Insert")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Insert([FromBody]Phrases phrases)
        {
            var result = phrasesRepository.Insert(phrases, _config.GetConnectionString("ExemploDapperSqlServer"));

            if (result)
                return StatusCode(201);
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("Delete")]
        public bool Delete(int Id)
        {
            return phrasesRepository.Delete(Id, _config.GetConnectionString("ExemploDapperSqlServer"));
        }

        [HttpPut]
        [Route("Update")]
        public bool Update([FromBody]Phrases phrases)
        {
            return phrasesRepository.Update(phrases, _config.GetConnectionString("ExemploDapperSqlServer"));
        }
    }
}