using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public class MakeMoveCommandHandler(ITicTacToeGameRepository repository, ITicTacToeGameService gameService) : IRequestHandler<MakeMoveCommand, Result<TicTacToeGame>>
{
    public async Task<Result<TicTacToeGame>> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var gameResult = await repository.GetAsync(request.GameId, cancellationToken);
        if (gameResult.IsSuccess is false) return Result<TicTacToeGame>.Failure(gameResult.Error);
        var game = gameResult.Value;
        var result = gameService.Move(game!, request.X, request.Y);

        if (!result.IsSuccess) return Result<TicTacToeGame>.Failure(result.Error);
        
        await repository.UpdateAsync(game!, cancellationToken);
        return Result<TicTacToeGame>.Success(game!);
    }
}