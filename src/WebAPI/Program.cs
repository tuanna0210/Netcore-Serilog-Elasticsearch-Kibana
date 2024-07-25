using Serilog;
using WebAPI.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
{
    //configuration.WriteTo.Elasticsearch();
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
