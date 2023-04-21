using Microsoft.EntityFrameworkCore;
using SchoolManagement.API.MapperProfiles;
using SchoolManagement.DAL;
using SchoolManagement.DAL.UnitOfWork;
using SchoolManagement.DAL.UnitOfWork.Interfaces;
using SchoolManagement.Services;
using SchoolManagement.Services.Interfaces;
using SchoolManagement.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SchoolDbContext>(
    options => options.UseSqlServer(
        connectionString: builder.Configuration["ConnectionStrings:DefaultConnection"],
        sqlServerOptionsAction: optionsBuilder => optionsBuilder.MigrationsAssembly("SchoolManagement.API")));

builder.Services.AddLogging();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new StudentMapperProfile());
    config.AddProfile(new TeacherMapperProfile());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(
    builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var provider = scope.ServiceProvider;
var databaseInitializer = provider.GetRequiredService<IDatabaseInitializer>();

try
{
    await databaseInitializer.InitializeAsync();
}
catch (Exception exception)
{
    var logger = provider.GetRequiredService<ILogger>();

    logger.LogError(exception, "Error on initializing database.");
}

app.Run();
