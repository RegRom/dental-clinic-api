using DentalClinicBLL.Interfaces;
using DentalClinicBLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DentalClinicBLL.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRouteServices(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IDentistRepository, DentistRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IProcedureRepository, ProcedureRepository>();

            return services;
        }
    }
}
