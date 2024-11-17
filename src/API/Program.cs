using API.Middleware;
using Application;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CostService>();
builder.Services.AddDb(configuration.GetConnectionString("PengaDb"));
builder.Services.AddValidatorsFromAssemblyContaining<AddOrUpdateCategoryRequestValidator>();
builder.Services.AddSerilog();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();