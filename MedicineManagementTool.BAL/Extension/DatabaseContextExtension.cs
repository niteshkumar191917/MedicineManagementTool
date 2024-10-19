using MedicineManagementTool.BAL.Authentication;
using MedicineManagementTool.BAL.IAuthenticate;
using MedicineManagementTool.BAL.IService;
using MedicineManagementTool.BAL.MappingDTO;
using MedicineManagementTool.BAL.Service;
using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.IUnitOfWork;
using MedicineManagementTool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicineManagementTool.BAL.Extension
{
    public static class DatabaseContextExtension
    {
        public static void AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            serviceCollection.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
            serviceCollection.AddAutoMapper(typeof(MapperDTO));

            serviceCollection.AddTransient<IWrapperRepository, WrapperRepository>();
            serviceCollection.AddScoped<IUserService,UserService>();           
            serviceCollection.AddScoped<IMedicineService,MedicineService>();           
            serviceCollection.AddScoped<IAuthenticateService,AuthenticateService>();           
            serviceCollection.AddScoped<ISaleDetailService,SaleDetailService>();           
            serviceCollection.AddSingleton<IEmailService,EmailService>();           

        }
    }
}
