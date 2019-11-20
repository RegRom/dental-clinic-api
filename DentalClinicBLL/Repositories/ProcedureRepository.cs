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
    public class ProcedureRepository : IProcedureRepository
    {
        private readonly DentalClinicContext _dentalClinicContext;
        private readonly IMapper _mapper;

        public ProcedureRepository(DentalClinicContext dentalClinicContext, IMapper mapper)
        {
            _dentalClinicContext = dentalClinicContext;
            _mapper = mapper;
        }

        public async Task<ProcedureDto> GetAsync(int id)
        {
            var procedure = await _dentalClinicContext.Procedures
                .FirstOrDefaultAsync(p => p.Id == id);

            if (procedure == null)
            {
                throw new KeyNotFoundException("Procedure with given id doesn't exist");
            }

            return _mapper.Map<ProcedureDto>(procedure);
        }

        public async Task<IEnumerable<ProcedureDto>> GetAllAsync()
        {
            var procedureList = await _dentalClinicContext.Procedures.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<ProcedureDto>>(procedureList);
        }

        public async Task<int> UpdateAsync(ProcedureDto procedureDto)
        {
            var procedure = await _dentalClinicContext.Procedures
                .FirstOrDefaultAsync(p => p.Id == procedureDto.Id);

            if (procedure == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            _mapper.Map(procedureDto, procedure);

            _dentalClinicContext.Update(procedure);

            return _dentalClinicContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var procedure = await _dentalClinicContext.Procedures
                .FirstOrDefaultAsync(p => p.Id == id);

            if (procedure == null)
            {
                throw new KeyNotFoundException("Patient with given id doesn't exist");
            }

            _dentalClinicContext.Remove(procedure);

            return await _dentalClinicContext.SaveChangesAsync();
        }

        public async Task<int> AddAsync(ProcedureDto procedureDto)
        {
            var procedure = _mapper.Map<Procedure>(procedureDto);

            _dentalClinicContext.Add(procedure);

            return await _dentalClinicContext.SaveChangesAsync();
        }
    }
}
