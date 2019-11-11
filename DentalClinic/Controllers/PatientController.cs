using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicBLL.Interfaces;
using DentalClinicBLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PatientDto>> Get()
        {
            var patients = await _patientRepository.GetAllAsync();

            return patients;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PatientDto>> Get(int id)
        {
            PatientDto patient;

            try
            {
                patient = await _patientRepository.GetAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post([FromBody] PatientDto patient)
        {
            int recordId;

            try
            {
                recordId = await _patientRepository.AddAsync(patient);
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
        public async Task<ActionResult> Put([FromBody] PatientDto patients)
        {
            try
            {
                await _patientRepository.UpdateAsync(patients);
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
                await _patientRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
