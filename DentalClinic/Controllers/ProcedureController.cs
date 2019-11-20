using DentalClinicBLL.Interfaces;
using DentalClinicBLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureController(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProcedureDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ProcedureDto>> Get()
        {
            var procedures = await _procedureRepository.GetAllAsync();

            return procedures;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcedureDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProcedureDto>> Get(int id)
        {
            ProcedureDto procedure;

            try
            {
                procedure = await _procedureRepository.GetAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return procedure;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post([FromBody] ProcedureDto procedure)
        {
            int recordId;

            try
            {
                recordId = await _procedureRepository.AddAsync(procedure);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return recordId;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] ProcedureDto procedure)
        {
            try
            {
                await _procedureRepository.UpdateAsync(procedure);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _procedureRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}