using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Core.Common;
using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Application.Interfaces;

public interface ITicTacToeGameService
{
    Result<bool, AppErrors> Move(TicTacToeGame ticTacToeGame, byte x, byte y);
}