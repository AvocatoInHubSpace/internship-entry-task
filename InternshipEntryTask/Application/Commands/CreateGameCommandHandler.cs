using InternshipEntryTask.Application.Settings;
using InternshipEntryTask.Core.Interfaces;
using InternshipEntryTask.Core.Models;
using MediatR;
using Microsoft.Extensions.Options;

namespace InternshipEntryTask.Application.Commands;

public class CreateGameCommandHandler(ITicTacToeGameRepository repository, IOptions<TicTacToeGameOptions> options) : IRequestHandler<CreateGameCommand, TicTacToeGame>
{
    public async Task<TicTacToeGame> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var game = new TicTacToeGame(options.Value.Size, options.Value.WinLineSize);
        await repository.AddAsync(game, cancellationToken);
        return game;
    }
}