using InternshipEntryTask.Core.Models;

namespace InternshipEntryTask.Presentation.DTOs;

public record GameResponse
{
    public int Id { get; init; }
    public byte Size { get; init; }
    public byte WinLineSize { get; init; }
    public bool IsXMove { get; init; } = true;
    public GameState State { get; init; } = GameState.InProgress;
    
    public string Field { get; init; } = String.Empty;
}