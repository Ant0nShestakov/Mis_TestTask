using Application.Conditions;
using Application.Patients;
using Application.Practitioners;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPractitionerService, PractitionerService>();
            services.AddScoped<IConditionService, ConditionService>();
            return services;
        }
    }
}
