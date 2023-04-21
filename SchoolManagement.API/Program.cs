using Microsoft.EntityFrameworkCore;
using SchoolManagement.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SchoolDbContext>(
    options => options.UseSqlServer(
        connectionString: builder.Configuration["ConnectionStrings:DefaultConnection"],
        sqlServerOptionsAction: optionsBuilder => optionsBuilder.MigrationsAssembly("SchoolManagement.API")));

builder.Services.AddLogging();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
