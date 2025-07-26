using InternshipEntryTask.Core.Common;

namespace InternshipEntryTask.Application.Common;

public static class ApplicationErrorMapper
{
    public static AppErrors ToAppError(this GameErrors error)
    {
        return error switch
        {
            GameErrors.GameIsEnd => AppErrors.GameIsEnd,
            _ => AppErrors.InvalidInputData
        };
    }
}