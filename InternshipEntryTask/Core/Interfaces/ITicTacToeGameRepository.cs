using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Core.Interfaces;

public interface ITicTacToeGameRepository
{
    Task<Result<TicTacToeGame>> GetAsync(int id);
    Task<Result<int>> AddAsync(TicTacToeGame game);
    Task<Result> UpdateAsync(TicTacToeGame game);
}