using Application.Common.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPractitionerRepository, PractitionerRepository>();
            services.AddScoped<IConditionRepository, ConditionRepository>();
            services.AddScoped<IEncounterRepository, EncounterRepository>();
            services.AddScoped<IObservationRepository, ObservationRepository>();
            return services;
        }
    }
}
