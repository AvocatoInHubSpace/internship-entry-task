using InternshipEntryTask.Application.DTOs;
using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public class MakeMoveCommandHandler(ITicTacToeGameRepository repository, ITicTacToeGameService gameService) : IRequestHandler<MakeMoveCommand, Result<MoveDto>>
{
    public async Task<Result<MoveDto>> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var gameResult = await repository.GetAsync(request.GameId, cancellationToken);
        if (gameResult.IsSuccess is false) return Result<MoveDto>.Failure(gameResult.Error);
        var game = gameResult.Value;
        var result = gameService.Move(game!, request.X, request.Y);

        if (!result.IsSuccess) return Result<MoveDto>.Failure(result.Error);
        
        await repository.UpdateAsync(game!, cancellationToken);
        
        var move = new MoveDto
        {
            Game = game!,
            IsXMove = result.Value!,
            X = request.X,
            Y = request.Y
        };
        return Result<MoveDto>.Success(move);
    }
}