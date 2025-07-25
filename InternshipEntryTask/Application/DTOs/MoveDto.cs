using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Application.DTOs;

public record MoveDto
{
    public required TicTacToeGame Game { get; init; }
    public byte X { get; init; }
    public byte Y { get; init; }
    public bool IsXMove { get; init; }
}