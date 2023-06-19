using DataAccess.ApplicDbContext;
using DependencyInfra;
using Microsoft.EntityFrameworkCore;
using Model.AutoMapper;
using MemberDto = Framework.DtoModels.Member;
using MemberModel = Model.Entities.Member;

namespace MVC_Gymclub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            },
            ServiceLifetime.Scoped);

            // DI admin: 
            DepInjection.SetDepInjectionLogic(builder.Services);
            DepInjection.SetDepInjectionData(builder.Services);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Member}/{action=Index}");

            MapperHelper.InitMapper(); 

            app.Run();
        }
    }
}