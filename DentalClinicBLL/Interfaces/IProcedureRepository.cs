using DentalClinicBLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinicBLL.Interfaces
{
    public interface IProcedureRepository
    {
        Task<ProcedureDto> GetAsync(int id);
        Task<IEnumerable<ProcedureDto>> GetAllAsync();
        Task<int> UpdateAsync(ProcedureDto procedureDto);
        Task<int> DeleteAsync(int id);
        Task<int> AddAsync(ProcedureDto procedureDto);
    }
}
