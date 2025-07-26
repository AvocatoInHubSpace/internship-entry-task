using InternshipEntryTask.Application.Common;

namespace InternshipEntryTask.Presentation.DTOs;

public class AppErrorFeature
{
    public AppErrors Error { get; init; }
    public string? ErrorMessage { get; init; }
    
}