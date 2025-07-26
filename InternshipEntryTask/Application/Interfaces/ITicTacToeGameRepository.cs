using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Application.Interfaces;

public interface ITicTacToeGameRepository
{
    Task<TicTacToeGame?> GetAsync(int id, CancellationToken ct);
    Task AddAsync(TicTacToeGame game, CancellationToken ct);
    Task UpdateAsync(TicTacToeGame game, CancellationToken ct);
}