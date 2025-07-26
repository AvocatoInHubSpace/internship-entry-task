using InternshipEntryTask.Application.Interfaces;
using InternshipEntryTask.Application.Services;
using InternshipEntryTask.Application.Settings;
using InternshipEntryTask.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace InternshipEntryTask.Presentation.Extensions;

public static class AddApplicationServicesExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IValidateOptions<TicTacToeGameOptions>, TicTacToeGameOptionsValidation>();
        services.AddScoped<ITicTacToeGameRepository, TicTacToeGameRepository>();
        services.AddScoped<ITicTacToeGameService, TicTacToeGameService>();
        services.AddSingleton<IRandomService, RandomService>();
    }
}