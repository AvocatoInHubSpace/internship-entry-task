using InternshipEntryTask.Core.Common;

namespace InternshipEntryTask.Application.Common;

public static class AppResultExtensions
{
    private static readonly Dictionary<AppErrors, string> Messages = new()
    {
        [AppErrors.GameNotFound] = "Game not found.",
        [AppErrors.InvalidInputData] = "Invalid input data.",
        [AppErrors.GameIsEnd] = "Game already finished."
    };
    
    public static Result<T, AppErrors> GetAppErrorResult<T>(this AppErrors error)
    {
        return Result<T, AppErrors>.Failure(error, Messages[error]);
    }
}