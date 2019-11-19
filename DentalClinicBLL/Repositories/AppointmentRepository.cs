using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinicBLL.Interfaces;
using DentalClinicBLL.Models;
using DentalClinicDAL;
using DentalClinicDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicBLL.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DentalClinicContext _dentalClinicContext;
        private readonly IMapper _mapper;

        public AppointmentRepository(DentalClinicContext dentalClinicContext, IMapper mapper)
        {
            _dentalClinicContext = dentalClinicContext;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> GetAsync(int id)
        {
            var appointment = await _dentalClinicContext.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .Include(a => a.Procedure)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment with given id doesn't exist");
            }

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointmentList = await _dentalClinicContext.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .Include(a => a.Procedure)
                .AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointmentList);
        }

        public async Task<int> UpdateAsync(AppointmentDto appointmentDto)
        {
            var appointment = await _dentalClinicContext.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .Include(a => a.Procedure)
                .FirstOrDefaultAsync(p => p.Id == appointmentDto.Id);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment with given id doesn't exist");
            }

            _mapper.Map(appointmentDto, appointment);

            try
            {
                if (appointmentDto.DentistId != appointment.Dentist.Id)
                    appointment.Dentist = await _dentalClinicContext.Dentists.SingleOrDefaultAsync(d => d.Id == appointmentDto.DentistId);

                if (appointmentDto.PatientId != appointment.Patient.Id)
                    appointment.Patient = await _dentalClinicContext.Patients.SingleOrDefaultAsync(d => d.Id == appointmentDto.PatientId);

                if (appointmentDto.ProcedureId != appointment.Procedure.Id)
                    appointment.Procedure = await _dentalClinicContext.Procedures.SingleOrDefaultAsync(d => d.Id == appointmentDto.ProcedureId);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            _dentalClinicContext.Update(appointment);

            return _dentalClinicContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var appointment = await _dentalClinicContext.Appointments
                .FirstOrDefaultAsync(p => p.Id == id);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment with given id doesn't exist");
            }

            _dentalClinicContext.Remove(appointment);

            return await _dentalClinicContext.SaveChangesAsync();
        }

        public async Task<int> AddAsync(AppointmentDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);

            try
            {
                appointment.Dentist = await _dentalClinicContext.Dentists.SingleOrDefaultAsync(d => d.Id == appointmentDto.DentistId);
                appointment.Patient = await _dentalClinicContext.Patients.SingleOrDefaultAsync(d => d.Id == appointmentDto.PatientId);
                appointment.Procedure = await _dentalClinicContext.Procedures.SingleOrDefaultAsync(d => d.Id == appointmentDto.ProcedureId);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            _dentalClinicContext.Add(appointment);

            return await _dentalClinicContext.SaveChangesAsync();
        }
    }
}
