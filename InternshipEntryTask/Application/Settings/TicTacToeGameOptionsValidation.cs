using InternshipEntryTask.Core.Validators;
using Microsoft.Extensions.Options;

namespace InternshipEntryTask.Application.Settings;

public class TicTacToeGameOptionsValidation : IValidateOptions<TicTacToeGameOptions>
{
    public ValidateOptionsResult Validate(string? name, TicTacToeGameOptions options)
    {
        var result = TicTacToeGameParametersValidator.Validate(options.Size, options.WinLineSize);
        return result.IsSuccess
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(result.Error!);
    }
}