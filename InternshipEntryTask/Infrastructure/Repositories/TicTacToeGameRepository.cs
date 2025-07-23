using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using InternshipEntryTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipEntryTask.Infrastructure.Repositories;

public class TicTacToeGameRepository(GameDbContext context) : ITicTacToeGameRepository
{

    public async Task<Result<TicTacToeGame>> GetAsync(int id, CancellationToken ct = default)
    {
        var result = await context.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id, cancellationToken: ct);
        return result is null
            ? Result<TicTacToeGame>.Failure("Game not found.")
            : Result<TicTacToeGame>.Success(result);
    }

    public async Task AddAsync(TicTacToeGame game, CancellationToken ct = default)
    {
        await context.Games.AddAsync(game, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(TicTacToeGame game, CancellationToken ct = default)
    {
        context.Games.Update(game);
        await context.SaveChangesAsync(ct);
    }
}