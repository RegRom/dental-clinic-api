using DentalClinicBLL.Interfaces;
using DentalClinicBLL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class DentistController : ControllerBase
    {
        private readonly IDentistRepository _dentistRepository;

        public DentistController(IDentistRepository dentistRepository)
        {
            _dentistRepository = dentistRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DentistDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<DentistDto>> Get()
        {
            var dentists = await _dentistRepository.GetAllAsync();

            return dentists;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DentistDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DentistDto>> Get(int id)
        {
            DentistDto dentist;

            try
            {
                dentist = await _dentistRepository.GetAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return dentist;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post([FromBody] DentistDto dentist)
        {
            int recordId;

            try
            {
                recordId = await _dentistRepository.AddAsync(dentist);
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
        public async Task<ActionResult> Put([FromBody] DentistDto dentist)
        {
            try
            {
                await _dentistRepository.UpdateAsync(dentist);
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
                await _dentistRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}