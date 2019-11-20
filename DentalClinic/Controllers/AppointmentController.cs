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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AppointmentDto>> Get()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            return appointments;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppointmentDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentDto>> Get(int id)
        {
            AppointmentDto appointment;

            try
            {
                appointment = await _appointmentRepository.GetAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return appointment;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post([FromBody] AppointmentDto appointment)
        {
            int recordId;

            try
            {
                recordId = await _appointmentRepository.AddAsync(appointment);
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
        public async Task<ActionResult> Put([FromBody] AppointmentDto appointment)
        {
            try
            {
                await _appointmentRepository.UpdateAsync(appointment);
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
                await _appointmentRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}