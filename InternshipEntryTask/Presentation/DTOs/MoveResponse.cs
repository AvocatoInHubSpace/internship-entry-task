using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Presentation.DTOs;

public record MoveResponse
{
    public int GameId { get; init; }
    public GameState State { get; init; } 
    public bool IsXMoved { get; init; }
    public byte X { get; init; }
    public byte Y { get; init; }
}