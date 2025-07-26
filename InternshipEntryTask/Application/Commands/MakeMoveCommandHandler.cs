using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Application.DTOs;
using InternshipEntryTask.Application.Interfaces;
using InternshipEntryTask.Core.Common;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public class MakeMoveCommandHandler(ITicTacToeGameRepository repository, ITicTacToeGameService gameService) : IRequestHandler<MakeMoveCommand, Result<MoveDto, AppErrors>>
{
    public async Task<Result<MoveDto, AppErrors>> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.GetAsync(request.GameId, cancellationToken);
        if (game is null) return AppErrors.GameNotFound.GetAppErrorResult<MoveDto>();
        var result = gameService.Move(game!, request.X, request.Y);

        if (!result.IsSuccess) return result.Error.GetAppErrorResult<MoveDto>();
        
        await repository.UpdateAsync(game!, cancellationToken);
        
        var move = new MoveDto
        {
            Game = game!,
            IsXMove = result.Value!,
            X = request.X,
            Y = request.Y
        };
        return Result<MoveDto, AppErrors>.Success(move);
    }
}