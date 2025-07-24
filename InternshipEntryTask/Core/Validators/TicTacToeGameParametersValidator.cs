using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Core.Validators;

public static class TicTacToeGameParametersValidator
{
    public static Result Validate(byte size, byte winLineSize)
    {
        if (size < 3)
            return Result.Failure($"Size must be between 3 and {byte.MaxValue}");
        if (winLineSize < 3)
            return Result.Failure($"WinLineSize must be between 3 and {byte.MaxValue}");
        if (winLineSize > size)
            return Result.Failure("WinLineSize must be less than or equal to Size");
        return Result.Success();
    }
}