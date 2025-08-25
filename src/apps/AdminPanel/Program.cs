using AdminPanel.Services.AttributeService;
using AdminPanel.Services.StatusService;

namespace AdminPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // --- StatusService --- \\
            builder.Services.AddHttpClient<StatusTitleAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:StatusService"]);
            });

            builder.Services.AddHttpClient<StatusTranslateAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:StatusService"]);
            });

            builder.Services.AddHttpClient<TypeTitleAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:StatusService"]);
            });

            // --- AttributeService --- \\
            builder.Services.AddHttpClient<ArtistAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:AttributeService"]);
            });

            builder.Services.AddHttpClient<AuthorAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:AttributeService"]);
            });
            builder.Services.AddHttpClient<GenreAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:AttributeService"]);
            });

            builder.Services.AddHttpClient<PublisherAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:AttributeService"]);
            });

            builder.Services.AddHttpClient<TagAPIClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExternalApi:AttributeService"]);
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller=Statuses}/{action=StatusTitle}/{id?}"
                ).WithStaticAssets();

            app.Run();
        }
    }
}
