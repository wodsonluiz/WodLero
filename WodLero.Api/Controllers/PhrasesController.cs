using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Phrases>> GetAll()
        {
            return await phrasesRepository.GetAll(_config.GetConnectionString("ExemploDapperSqlServer"));
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public async Task<Phrases> GetById(int Id)
        {
            return await phrasesRepository.GetById(Id, _config.GetConnectionString("ExemploDapperSqlServer"));
        }

        /// <summary>
        /// Insert a Phrases.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "Id": 0,
        ///        "Descricao":"",
        ///        "Status":"",
        ///        "Data_Registro":"",
        ///        "Autor":""
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Phrases</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [Route("Insert")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Insert([FromBody]Phrases phrases)
        {
            var result = await phrasesRepository.Insert(phrases, _config.GetConnectionString("ExemploDapperSqlServer"));

            if (result)
                return StatusCode(201);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete Phrases
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(int Id)
        {
            return await phrasesRepository.Delete(Id, _config.GetConnectionString("ExemploDapperSqlServer"));
        }

        /// <summary>
        /// Update a Phrases.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    POST /Todo
        ///     {
        ///        "Id": 0,
        ///        "Descricao":"",
        ///        "Status":"",
        ///        "Data_Registro":"",
        ///        "Autor":""
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly Update Phrases</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPut]
        [Route("Update")]
        public async Task<bool> Update([FromBody]Phrases phrases)
        {
            return await phrasesRepository.Update(phrases, _config.GetConnectionString("ExemploDapperSqlServer"));
        }
    }
}