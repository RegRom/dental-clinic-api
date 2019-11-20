using DentalClinicBLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinicBLL.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<AppointmentDto> GetAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<int> UpdateAsync(AppointmentDto appointmentDto);
        Task<int> DeleteAsync(int id);
        Task<int> AddAsync(AppointmentDto appointmentDto);
    }
}
