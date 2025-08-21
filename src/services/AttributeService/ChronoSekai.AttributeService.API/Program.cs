using ChronoSekai.AttributeService.Application.Ioc;
using ChronoSekai.AttributeService.Infrastructure.Ioc;
using ChronoSekai.AttributeService.Application.Common;

namespace ChronoSekai.AttributeService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(IApplicationDbContext).Assembly));
            builder.Services.AddAutoMapper(cfg => { cfg.LicenseKey = builder.Configuration["AutoMapper:AutoMapperKey"]; }, typeof(IApplicationDbContext).Assembly);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
