using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Core.Interfaces;

public interface ITicTacToeGameService
{
    Result<bool> Move(TicTacToeGame ticTacToeGame, byte x, byte y);
}