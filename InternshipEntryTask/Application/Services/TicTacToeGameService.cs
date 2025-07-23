using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Application.Services;

public class TicTacToeGameService(IRandomService random) : ITicTacToeGameService
{
    private const byte Probability = 10;

    public Result Move(TicTacToeGame game, byte x, byte y)
    {
        return game.Move(x, y, () => random.NextProbability(Probability));
    }
}