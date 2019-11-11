using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PatientRepository : IPatientRepository
    {
        private readonly DentalClinicContext _dentalClinicContext;
        private readonly IMapper _mapper;

        public PatientRepository(DentalClinicContext dentalClinicContext, IMapper mapper)
        {
            _dentalClinicContext = dentalClinicContext;
            _mapper = mapper;
        }

        public async Task<PatientDto> GetAsync(int id)
        {

            var patient = await _dentalClinicContext.Patients
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<IEnumerable<PatientDto>> GetAllAsync()
        {
            var patientList = await _dentalClinicContext.Patients.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<PatientDto>>(patientList);
        }

        public async Task<int> UpdateAsync(PatientDto patientDto)
        {
            var patient = await _dentalClinicContext.Patients
                .FirstOrDefaultAsync(p => p.Id == patientDto.Id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            _mapper.Map(patientDto, patient);

            _dentalClinicContext.Update(patient);

            return _dentalClinicContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var patient = await _dentalClinicContext.Patients
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            _dentalClinicContext.Remove(patient);

            return await _dentalClinicContext.SaveChangesAsync();
        }

        public async Task<int> AddAsync(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);

            _dentalClinicContext.Add(patient);

            return await _dentalClinicContext.SaveChangesAsync();
        }
    }
}
