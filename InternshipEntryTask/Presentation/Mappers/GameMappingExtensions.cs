using InternshipEntryTask.Core.Models;
using InternshipEntryTask.Presentation.DTOs;

namespace InternshipEntryTask.Presentation.Mappers;

public static class GameMappingExtensions
{
    public static GameResponse MapToGameResponse(this TicTacToeGame game)
    {
        return new GameResponse
        {
            Id = game.Id,
            Size = game.Size,
            WinLineSize = game.WinLineSize,
            IsXMove = game.IsXMove,
            State = game.State,
            Field = game.Field,
        };
    }
}