using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using MediatR;

namespace InternshipEntryTask.Application.Commands;

public class GetGameCommandHandler(ITicTacToeGameRepository repository) : IRequestHandler<GetGameCommand, Result<TicTacToeGame>>
{
    public async Task<Result<TicTacToeGame>> Handle(GetGameCommand request, CancellationToken cancellationToken)
    {
        return await repository.GetAsync(request.Id, cancellationToken);
    }
}