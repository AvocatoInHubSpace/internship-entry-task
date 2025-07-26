using InternshipEntryTask.Presentation.DTOs;

namespace InternshipEntryTask.Presentation.Extensions;

public static class CustomProblemDetailsExtension
{
    public static void AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options => options.CustomizeProblemDetails = (context) =>
        {
            var appErrorFeature = context.HttpContext.Features
                .Get<AppErrorFeature>();
            if (appErrorFeature is null) return;
    
            context.ProblemDetails.Type = appErrorFeature.Error.ToString();
            context.ProblemDetails.Title = "Application Error";
            context.ProblemDetails.Detail = appErrorFeature.ErrorMessage;
            context.ProblemDetails.Instance = context.HttpContext.Request.Path;
        });
    }
}