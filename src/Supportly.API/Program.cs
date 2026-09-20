using Microsoft.EntityFrameworkCore;
using Supportly.API.Extensions;
using Supportly.BusinessObjects;
using Supportly.Repositories.Implementation;
using Supportly.Repositories.Interface;
using Supportly.Services.Implementations;
using Supportly.Services.Interfaces;
using Supportly.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerDocs();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<SupportlyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Supportly"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

IncidentMappingConfig.Register();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwaggerDocs();
app.UseHttpsRedirection();

app.MapControllers();

// Apply pending migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<SupportlyDbContext>();
    db.Database.Migrate();
}

app.Run();

