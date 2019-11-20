using AutoMapper;
using DentalClinicBLL.Interfaces;
using DentalClinicBLL.Models;
using DentalClinicDAL;
using DentalClinicDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinicBLL.Repositories
{
    public class DentistRepository : IDentistRepository
    {
        private readonly DentalClinicContext _dentalClinicContext;
        private readonly IMapper _mapper;

        public DentistRepository(DentalClinicContext dentalClinicContext, IMapper mapper)
        {
            _dentalClinicContext = dentalClinicContext;
            _mapper = mapper;
        }

        public async Task<DentistDto> GetAsync(int id)
        {
            var dentist = await _dentalClinicContext.Dentists
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dentist == null)
            {
                throw new KeyNotFoundException("Dentist with given id doesn't exist");
            }

            return _mapper.Map<DentistDto>(dentist);
        }

        public async Task<IEnumerable<DentistDto>> GetAllAsync()
        {
            var dentistList = await _dentalClinicContext.Dentists.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<DentistDto>>(dentistList);
        }

        public async Task<int> UpdateAsync(DentistDto dentistDto)
        {
            var dentist = await _dentalClinicContext.Dentists
                .FirstOrDefaultAsync(p => p.Id == dentistDto.Id);

            if (dentist == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            _mapper.Map(dentistDto, dentist);

            _dentalClinicContext.Update(dentist);

            return _dentalClinicContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var dentist = await _dentalClinicContext.Dentists
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dentist == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            _dentalClinicContext.Remove(dentist);

            return await _dentalClinicContext.SaveChangesAsync();
        }

        public async Task<int> AddAsync(DentistDto dentistDto)
        {
            var dentist = _mapper.Map<Dentist>(dentistDto);

            _dentalClinicContext.Add(dentist);

            return await _dentalClinicContext.SaveChangesAsync();
        }
    }
}
