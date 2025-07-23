using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Core.Interfaces;

public interface ITicTacToeGameService
{
    Result Move(TicTacToeGame ticTacToeGame, byte x, byte y);
}