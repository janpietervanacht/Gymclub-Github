using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using LogicMember.Services.Implementations;
using LogicMember.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInfra
{
    public static class DepInjection
    {
        public static void SetDepInjectionData(IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<ITrainerCertificateRepository, TrainerCertificateRepository>();
        }

        public static void SetDepInjectionLogic(IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ITrainerService, MemberService>();
        }
    }
}