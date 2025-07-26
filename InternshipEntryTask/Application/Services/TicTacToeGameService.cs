using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Application.Interfaces;
using InternshipEntryTask.Core.Common;
using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Application.Services;

public class TicTacToeGameService(IRandomService random) : ITicTacToeGameService
{
    private const byte Probability = 10;

    public Result<bool, AppErrors> Move(TicTacToeGame game, byte x, byte y)
    {
        var gameResult = game.Move(x, y, () => random.NextProbability(Probability));
        return gameResult.IsSuccess is true 
            ? Result<bool, AppErrors>.Success(gameResult.Value!)
            : Result<bool, AppErrors>.Failure(gameResult.Error.ToAppError(), gameResult.ErrorMessage);
    }
}