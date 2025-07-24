using InternshipEntryTask.Infrastructure.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddNpgsql<GameDbContext>(connectionString); 

builder.Services.AddSingleton<IValidateOptions<TicTacToeGameOptions>, TicTacToeGameOptionsValidation>();
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

app.UseRouting();
app.MapControllers();
app.Run();