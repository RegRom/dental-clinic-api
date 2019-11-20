using DentalClinicBLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinicBLL.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDto> GetAsync(int id);
        Task<IEnumerable<PatientDto>> GetAllAsync();
        Task<int> UpdateAsync(PatientDto patientDto);
        Task<int> DeleteAsync(int id);
        Task<int> AddAsync(PatientDto patientDto);
    }
}
