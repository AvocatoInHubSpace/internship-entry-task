using InternshipEntryTask.Application.Common;
using InternshipEntryTask.Application.Interfaces;
using InternshipEntryTask.Core.Common;
using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public class GetGameCommandHandler(ITicTacToeGameRepository repository) : IRequestHandler<GetGameCommand, Result<TicTacToeGame, AppErrors>>
{
    public async Task<Result<TicTacToeGame, AppErrors>> Handle(GetGameCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAsync(request.Id, cancellationToken);
        return result is not null
            ? Result<TicTacToeGame, AppErrors>.Success(result)
            : AppErrors.GameNotFound.GetAppErrorResult<TicTacToeGame>();
    }
}