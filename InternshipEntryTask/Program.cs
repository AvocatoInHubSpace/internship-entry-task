using System.Text.Json.Serialization;
using InternshipEntryTask.Application.Services;
using InternshipEntryTask.Application.Settings;
using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Infrastructure.Data;
using InternshipEntryTask.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddNpgsql<GameDbContext>(connectionString);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSingleton<IValidateOptions<TicTacToeGameOptions>, TicTacToeGameOptionsValidation>();
builder.Services.AddScoped<ITicTacToeGameRepository, TicTacToeGameRepository>();
builder.Services.AddScoped<ITicTacToeGameService, TicTacToeGameService>();
builder.Services.AddSingleton<IRandomService, RandomService>();
builder.Services.AddOptions<TicTacToeGameOptions>()
    .Bind(builder.Configuration.GetSection(TicTacToeGameOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });
app.UseRouting();
app.MapControllers();
app.Run();