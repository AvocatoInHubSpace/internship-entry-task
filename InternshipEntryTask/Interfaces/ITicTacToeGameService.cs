using InternshipEntryTask.Infrastructure;
using InternshipEntryTask.Models;

namespace InternshipEntryTask.Interfaces;

public interface ITicTacToeGameService
{
    Result Move(TicTacToeGame ticTacToeGame, byte x, byte y);
}