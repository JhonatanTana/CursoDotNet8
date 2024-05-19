using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(mySqlConnection, 
    ServerVersion.AutoDetect(mySqlConnection)));

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration {

    LogLevel = LogLevel.Information

}));

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
