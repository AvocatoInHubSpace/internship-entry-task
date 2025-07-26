using System.Text.Json.Serialization;
using InternshipEntryTask.Application.Settings;
using InternshipEntryTask.Infrastructure.Data;
using InternshipEntryTask.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddNpgsql<GameDbContext>(connectionString);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddCustomProblemDetails();

builder.Services.AddApplicationServices();

builder.Services.AddOptions<TicTacToeGameOptions>()
    .Bind(builder.Configuration.GetSection(TicTacToeGameOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}
app.MapGet("/health", () => Results.Ok());
app.MapControllers();
app.Run();