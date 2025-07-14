namespace InternshipEntryTask.Models;

public class TicTacToeGame
{
    public int Id { get; init; }
    public required byte Size { get; init; }
    public required byte WinLineSize { get; init; }
    public bool IsXMove { get; set; } = true;

    public ushort MovesCount { get; set; } = 0;
    public GameState State { get; set; } = GameState.Process;
    
    public string Field { get; set; } = string.Empty;
}