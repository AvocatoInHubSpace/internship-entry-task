using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Core.Interfaces;

public interface ITicTacToeGameRepository
{
    Task<Result<TicTacToeGame>> GetAsync(int id, CancellationToken ct);
    Task AddAsync(TicTacToeGame game, CancellationToken ct);
    Task UpdateAsync(TicTacToeGame game, CancellationToken ct);
}