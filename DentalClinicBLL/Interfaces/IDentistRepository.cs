using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DentalClinicBLL.Models;

namespace DentalClinicBLL.Interfaces
{
    public interface IDentistRepository
    {
        Task<DentistDto> GetAsync(int id);
        Task<IEnumerable<DentistDto>> GetAllAsync();
        Task<int> UpdateAsync(DentistDto dentistDto);
        Task<int> DeleteAsync(int id);
        Task<int> AddAsync(DentistDto dentistDto);
    }
}
