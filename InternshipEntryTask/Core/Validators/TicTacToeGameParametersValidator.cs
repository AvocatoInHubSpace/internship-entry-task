
namespace InternshipEntryTask.Core.Validators;

public static class TicTacToeGameParametersValidator
{
    public static (bool IsSuccess, string? Error) Validate(byte size, byte winLineSize)
    {
        if (size < 3)
            return (false, $"Size must be between 3 and {byte.MaxValue}");
        if (winLineSize < 3)
            return (false, $"WinLineSize must be between 3 and {byte.MaxValue}");
        if (winLineSize > size)
            return (false, $"WinLineSize must be less than or equal to Size");
        return (true, null);
    }
}