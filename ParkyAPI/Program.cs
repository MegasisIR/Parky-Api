using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ParkyAPI.Data;
using ParkyAPI.Mappers;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ParkDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

AddInjectionRepository(builder.Services);
ConfigureServices(builder.Services);
ConfigureVersioning(builder.Services);
builder.Services.AddControllers();

ConfigureServicesSwagger(builder.Services);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    ConfigureAppSwagger(app);
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



void AddInjectionRepository(IServiceCollection services)
{
    services.AddScoped<INationalParkRepository, NationalParkRepository>();
    services.AddScoped<ITrailRepository, TrailRepository>();
}
void ConfigureServices(IServiceCollection services)
{
    services.AddAutoMapper(typeof(ParkyMappings));
}


#region Versioning
void ConfigureVersioning(IServiceCollection services)
{
    services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion= new ApiVersion(1, 0);
        options.ReportApiVersions = true;
    });
}
#endregion

#region Swagger Configure

void ConfigureServicesSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("ParkyApi",
            new OpenApiInfo()
            {
                Title = "Parky Api",
                Version = "v1",
                Description = "web api national park information",
                Contact = new OpenApiContact()
                {
                    Email = "Mehrdadit12@gmail.com",
                    Name = "Mehrdad Tabesh",
                    Url = new UriBuilder("mehrdadtabesh.ir").Uri
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT License",
                    Url =new UriBuilder("https://en.wikipedia.org/wiki/MIT_License").Uri
                }
            });
        var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
        options.IncludeXmlComments(cmlCommentsFullPath);
    });
}

void ConfigureAppSwagger(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/ParkyApi/swagger.json", "Parky Api");
    });
}
#endregion