namespace InternshipEntryTask.Application.Settings;

public class TicTacToeGameOptions
{
    public const string SectionName = "TicTacToeGame";
    
    public byte Size { get; init; }
    
    public byte WinLineSize { get; init; }
}