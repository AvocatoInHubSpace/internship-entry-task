using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Core.Common;
using InternshipEntryTask.Presentation.DTOs;

namespace InternshipEntryTask.Presentation.Mappers;

public static class AppErrorMapping
{
    public static AppErrorFeature MapToAppErrorFeature<T>(this Result<T, AppErrors> error) => new AppErrorFeature()
    {
        Error = error.Error,
        ErrorMessage = error.ErrorMessage
    };
}